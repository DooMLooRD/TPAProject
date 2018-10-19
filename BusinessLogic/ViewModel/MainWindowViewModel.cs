using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BusinessLogic.Model;

namespace BusinessLogic.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        public ICommand ClickOpen { get; }
        public IPathLoader PathLoader { get; set; }
        private AssemblyModel _assemblyMetadata;
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
                _assemblyMetadata = new AssemblyModel(Assembly.LoadFrom(PathVariable));
                _viewModelAssemblyMetadata = new AssemblyTreeItem(_assemblyMetadata);
                LoadTreeView();
            }

        }

        private void LoadTreeView()
        {
            TreeViewItem rootItem = new TreeViewItem(_viewModelAssemblyMetadata.Name, ItemTypeEnum.Assembly, _viewModelAssemblyMetadata);
            HierarchicalAreas.Add(rootItem);
        }
    }
}
