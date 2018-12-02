using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows.Input;
using BusinessLogic.Reflection;
using MEF;
using Ninject;
using ViewModels.Base;
using ViewModels.TreeViewItems;

namespace ViewModels.Pages
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Commands

        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }

        #endregion

        #region Mef

        [Import(typeof(IPathLoader))]
        public IPathLoader PathLoader { get; set; }
        [Import(typeof(ILogFactory))]
        public ILogFactory LoggerFactory { get; set; }
        [Import(typeof(ISerializer))]
        public ISerializer Serializer { get; set; }

        #endregion

        #region Fields

        private Reflector _reflector;
        private AssemblyTreeItem _viewModelAssemblyMetadata;
        private string _pathVariable;

        #endregion

        #region Properties

        [Inject]
        public string SerializePath { get; set; }
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }

        public string PathVariable
        {
            get => _pathVariable;
            set
            {
                _pathVariable = value;
                OnPropertyChanged(nameof(PathVariable));
            }
        }

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            OpenCommand = new RelayCommand(Open);
            SaveCommand = new RelayCommand(Save);
        }

        #endregion

        #region Methods

        private void Save()
        {
            LoggerFactory.Log(new MessageStructure("Serialize started..."));
            if (SerializePath != null)
            {
                Serializer.Save(_reflector.AssemblyModel, SerializePath);
                LoggerFactory.Log(new MessageStructure("Serialize completed"), LogCategoryEnum.Success);
            }
            else
            {
                LoggerFactory.Log(new MessageStructure("Serialize failed-Path is null"), LogCategoryEnum.Error);
            }

        }
        private void Open()
        {
            LoggerFactory.Log(new MessageStructure("Loading Path..."));

            string path = PathLoader.LoadPath();
            if (path != null)
            {
                if (path.Contains(".dll"))
                {
                    LoggerFactory.Log(new MessageStructure("Path Loaded"), LogCategoryEnum.Success);

                    PathVariable = path;
                    try
                    {
                        LoggerFactory.Log(new MessageStructure("Reflection started..."));
                        _reflector = new Reflector(Assembly.LoadFrom(PathVariable));
                    }
                    catch (Exception e)
                    {
                        LoggerFactory.Log(new MessageStructure("Reflection failed"), LogCategoryEnum.Error);
                    }

                    LoggerFactory.Log(new MessageStructure("Reflection success"), LogCategoryEnum.Success);

                    _viewModelAssemblyMetadata = new AssemblyTreeItem(_reflector.AssemblyModel);
                    LoadTreeView();
                }

                else if (path.Contains(".xml"))
                {
                    LoggerFactory.Log(new MessageStructure("Path Loaded"), LogCategoryEnum.Success);

                    PathVariable = path;
                    try
                    {
                        LoggerFactory.Log(new MessageStructure("Deserialization started..."));
                        _reflector = new Reflector(Serializer.Read(path));
                    }
                    catch (Exception e)
                    {
                        LoggerFactory.Log(new MessageStructure("Deserialization failed"), LogCategoryEnum.Error);
                    }

                    LoggerFactory.Log(new MessageStructure("Deserialization success"), LogCategoryEnum.Success);

                    _viewModelAssemblyMetadata = new AssemblyTreeItem(_reflector.AssemblyModel);
                    LoadTreeView();
                }
                else
                    LoggerFactory.Log(new MessageStructure("Reflection failed"), LogCategoryEnum.Error);
            }
            else
            {
                LoggerFactory.Log(new MessageStructure("Path not loaded"), LogCategoryEnum.Error);
            }

        }

        private void LoadTreeView()
        {
            TreeViewItem rootItem = _viewModelAssemblyMetadata;
            HierarchicalAreas.Add(rootItem);
        }

        #endregion

    }
}
