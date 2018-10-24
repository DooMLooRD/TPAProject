using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using BusinessLogic.Model;
using BusinessLogic.Reflection;
using BusinessLogic.Tracing;
using BusinessLogic.ViewModel.TreeViewItems;


namespace BusinessLogic.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        public ICommand ClickOpen { get; }
        public IPathLoader PathLoader { get; set; }
        public TraceManager Logger { get; set; }
        private Reflector _reflector;
        private AssemblyTreeItem _viewModelAssemblyMetadata;

        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }

        public string PathVariable { get; set; }

        public MainWindowViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            ClickOpen = new RelayCommand(Open);
        }

        private void Open()
        {
            Logger.Log("Loading path...",LogCategoryEnum.Information);

            string path = PathLoader.LoadPath();
            if (path!=null)
            {
                Logger.Log("Path loaded",LogCategoryEnum.Success);

                PathVariable = path;
                OnPropertyChanged("PathVariable");
                try
                {
                    Logger.Log("Reflection started...",LogCategoryEnum.Information);
                    _reflector = new Reflector(Assembly.LoadFrom(PathVariable));
                }
                catch (Exception e)
                {
                    Logger.Log("Reflection failed",LogCategoryEnum.Error);
                }
                
                Logger.Log("Reflection success",LogCategoryEnum.Success);

                _viewModelAssemblyMetadata = new AssemblyTreeItem(_reflector.AssemblyModel);
                LoadTreeView();
            }
            else
            {
                Logger.Log("Path not loaded",LogCategoryEnum.Error);
            }

        }

        private void LoadTreeView()
        {
            TreeViewItem rootItem = _viewModelAssemblyMetadata;
            HierarchicalAreas.Add(rootItem);
        }
    }
}
