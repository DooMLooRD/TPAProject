using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLogic.Logging;
using BusinessLogic.Reflection;
using BusinessLogic.Serialization;
using BusinessLogic.ViewModel.TreeViewItems;
using Ninject;

namespace BusinessLogic.ViewModel.Pages
{
    public class TreeViewViewModel : BaseViewModel
    {
        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        [Inject]
        public IPathLoader PathLoader { get; set; }
        [Inject]
        public ILogFactory LoggerFactory { get; set; }
        [Inject]
        public ISerializer Serializer { get; set; }
        public string SerializePath { get; set; }
        private Reflector _reflector;
        private AssemblyTreeItem _viewModelAssemblyMetadata;
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        public string PathVariable { get; set; }

        public TreeViewViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            OpenCommand = new RelayCommand(Open);
            SaveCommand = new RelayCommand(Save);
        }

        private void Save()
        {
            LoggerFactory.Log(new MessageStructure("Serialize started..."));
            if (SerializePath != null)
            {
                Serializer.Serialize(_reflector.AssemblyModel, SerializePath);
                LoggerFactory.Log(new MessageStructure("Serialize completed"),LogCategoryEnum.Success);
            }
            else
            {
                LoggerFactory.Log(new MessageStructure("Serialize failed-Path is null"),LogCategoryEnum.Error);
            }
            
        }
        private void Open()
        {
            LoggerFactory.Log(new MessageStructure("Loading Path..."));

            string path = PathLoader.LoadPath();
            if (path != null)
            {
                LoggerFactory.Log(new MessageStructure("Path Loaded"), LogCategoryEnum.Success);

                PathVariable = path;
                OnPropertyChanged("PathVariable");
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
    }
}
