using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using BusinessLogic.Logging;
using BusinessLogic.Model;
using BusinessLogic.Reflection;
using BusinessLogic.ViewModel.Pages;
using BusinessLogic.ViewModel.TreeViewItems;
using Ninject;


namespace BusinessLogic.ViewModel.Pages
{
    public class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _currentPage;
        private TreeViewViewModel _treeViewViewModel;
        private SettingsViewModel _settingsViewModel;

        public SettingsViewModel SettingsViewModel
        {
            get => _settingsViewModel;
            set => _settingsViewModel = value;
        }

        public TreeViewViewModel TreeViewViewModel
        {
            get => _treeViewViewModel;
            set => _treeViewViewModel = value;
        }

        public BaseViewModel CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value; 
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public ICommand ClickOpen { get; }
        public ICommand SettingsOpen { get; }
             

        public MainWindowViewModel(TreeViewViewModel vm,SettingsViewModel sw)
        {
            TreeViewViewModel = vm;
            SettingsViewModel = sw;
            ClickOpen = new RelayCommand(Open);
            SettingsOpen= new RelayCommand(Settings);
            SettingsViewModel.LoadSettings();
        }

        private void Open()
        {
            CurrentPage = TreeViewViewModel;
        }

        private void Settings()
        {
            CurrentPage = SettingsViewModel;
            SettingsViewModel.LoadSettings();
        }

    }
}
