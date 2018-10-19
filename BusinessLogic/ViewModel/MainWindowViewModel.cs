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
using Microsoft.Win32;

namespace BusinessLogic.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        public ICommand Click_Open { get; }
        public ICommand Click_Show { get; }

        private AssemblyModel assemblyMetadata;
        private AssemblyTreeItem viewModelAssemblyMetadata;

        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }

        public string PathVariable { get; set; }

        public MainWindowViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            Click_Open = new RelayCommand(Open);
            Click_Show = new RelayCommand(Load);
        }

        private void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Dynamic Library File(*.dll)| *.dll|"
                       + "Executable File(*.exe)| *.exe|"
                       + "XML File(*.xml)| *.xml",
                RestoreDirectory = true
            };
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
            }
            else
            {
                PathVariable = openFileDialog.FileName;
                OnPropertyChanged("PathVariable");
            }
        }



        private void Load()
        {
            if (PathVariable.Contains(".dll"))
            {
                assemblyMetadata = new AssemblyModel(Assembly.LoadFrom(PathVariable));
            }
            viewModelAssemblyMetadata = new AssemblyTreeItem(assemblyMetadata);
            LoadTreeView();
        }

        private void LoadTreeView()
        {
            TreeViewItem rootItem = new TreeViewItem(viewModelAssemblyMetadata.Name, ItemTypeEnum.Assembly, viewModelAssemblyMetadata);
            HierarchicalAreas.Add(rootItem);
        }
    }
}
