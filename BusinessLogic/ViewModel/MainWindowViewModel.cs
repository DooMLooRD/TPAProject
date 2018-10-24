using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using BusinessLogic.Model;
using BusinessLogic.Reflection;
using BusinessLogic.ViewModel.TreeViewItems;


namespace BusinessLogic.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        public ICommand ClickOpen { get; }
        public IPathLoader PathLoader { get; set; }
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
            string path = PathLoader.LoadPath();
            if (path!=null)
            {
                PathVariable = path;
                OnPropertyChanged("PathVariable");
                _reflector = new Reflector(Assembly.LoadFrom(PathVariable));
                _viewModelAssemblyMetadata = new AssemblyTreeItem(_reflector.AssemblyModel);
                LoadTreeView();
            }

        }

        private void LoadTreeView()
        {
            TreeViewItem rootItem = _viewModelAssemblyMetadata;
            HierarchicalAreas.Add(rootItem);
        }
    }
}
