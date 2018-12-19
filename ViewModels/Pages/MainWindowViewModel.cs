using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLogic;
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
        public ICommand LoadCommand { get; }

        #endregion

        #region Mef

        [Import(typeof(IPathLoader))]
        public IPathLoader PathLoader { get; set; }
        [Import(typeof(ILogger))]
        public ILogger Logger { get; set; }
        [Import(typeof(IInformationDisplay))]
        public IInformationDisplay DisplayInfo { get; set; }

        #endregion

        #region Fields

        private Reflector _reflector;
        private AssemblyTreeItem _viewModelAssemblyMetadata;
        private string _pathVariable;
        private ObservableCollection<TreeViewItem> _hierarchicalAreas;
        private bool _isBusy;
        private string _busyContent;

        #endregion

        #region Properties
        [ImportMany(typeof(LogicService))]
        public IEnumerable<LogicService> Service { get; set; }

        public ObservableCollection<TreeViewItem> HierarchicalAreas
        {
            get => _hierarchicalAreas;
            set
            {
                _hierarchicalAreas = value;
                OnPropertyChanged(nameof(HierarchicalAreas));
            }
        }

        public string PathVariable
        {
            get => _pathVariable;
            set
            {
                _pathVariable = value;
                OnPropertyChanged(nameof(PathVariable));
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        public string BusyContent
        {
            get => _busyContent;
            set
            {
                _busyContent = value;
                OnPropertyChanged(nameof(BusyContent));
            }
        }

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            OpenCommand = new RelayCommand(async () => await Task.Run(() => { Open(); }));
            SaveCommand = new RelayCommand(async () => await Task.Run(() => { Save(); }));
            LoadCommand = new RelayCommand(async () => await Task.Run(() => { Load(); }));
        }

        private void Load()
        {
            BusyContent = "Loading...";
            IsBusy = true;
            Logger.Log(new MessageStructure("Deserialization started..."));
            string path = ConfigurationManager.AppSettings["serializationFilename"];
            try
            {               
                _reflector = new Reflector(Service.ToList().FirstOrDefault()?.Load(path));

                IsBusy = false;
                Logger.Log(new MessageStructure("Deserialization success"), LogCategoryEnum.Success);
                DisplayInfo.ShowInfo("Deserialization success");

                _viewModelAssemblyMetadata = new AssemblyTreeItem(_reflector.AssemblyModel);
                HierarchicalAreas = new ObservableCollection<TreeViewItem> { _viewModelAssemblyMetadata };
            }
            catch (Exception e)
            {
                IsBusy = false;
                Logger.Log(new MessageStructure("Deserialization failed "+e.Message), LogCategoryEnum.Error);
                DisplayInfo.ShowInfo("Deserialization failed "+e.Message);
            }

        }

        #endregion

        #region Methods

        private void Save()
        {
            BusyContent = "Saving...";
            IsBusy = true;
            Logger.Log(new MessageStructure("Serialize started..."));
            string serializePath = ConfigurationManager.AppSettings["serializationFilename"];
            if (serializePath != null)
            {
                Service.ToList().FirstOrDefault()?.Save(_reflector.AssemblyModel, serializePath);
                IsBusy = false;
                Logger.Log(new MessageStructure("Serialization completed"), LogCategoryEnum.Success);
                DisplayInfo.ShowInfo("Serialization Completed");
            }
            else
            {
                IsBusy = false;
                Logger.Log(new MessageStructure("Serialization failed-Path is null"), LogCategoryEnum.Error);
                DisplayInfo.ShowInfo("Serialization failed - path is null");
            }
            
        }
        private void Open()
        {
            BusyContent = "Opening...";
            IsBusy = true;
            Logger.Log(new MessageStructure("Loading Path..."));

            string path = PathLoader.LoadPath();
            if (path != null && path.Contains(".dll"))
            {

                Logger.Log(new MessageStructure("Path Loaded"), LogCategoryEnum.Success);

                PathVariable = path;
                try
                {
                    Logger.Log(new MessageStructure("Reflection started..."));
                    _reflector = new Reflector(Assembly.LoadFrom(PathVariable));
                }
                catch (Exception e)
                {
                    IsBusy = false;
                    Logger.Log(new MessageStructure("Reflection failed"), LogCategoryEnum.Error);
                    DisplayInfo.ShowInfo("Reflection failed");
                }
                _viewModelAssemblyMetadata = new AssemblyTreeItem(_reflector.AssemblyModel);
                HierarchicalAreas = new ObservableCollection<TreeViewItem> { _viewModelAssemblyMetadata };
                IsBusy = false;
                Logger.Log(new MessageStructure("Reflection success"), LogCategoryEnum.Success);
                DisplayInfo.ShowInfo("Reflection success");
            }
            else
            {
                IsBusy = false;
                Logger.Log(new MessageStructure("Path not loaded or wrong format"), LogCategoryEnum.Error);
            }
            
        }


        #endregion

    }
}
