using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using BusinessLogic.DI.Interfaces;
using BusinessLogic.Logging;
using BusinessLogic.Model;
using BusinessLogic.Reflection;
using BusinessLogic.ViewModel.Pages;
using BusinessLogic.ViewModel.TreeViewItems;


namespace BusinessLogic.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _currentPage;
        public SettingsViewModel SettingsViewModel { get; set; }
        public TreeViewViewModel TreeViewViewModel { get; set; }

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
             

        public MainWindowViewModel()
        {
            ClickOpen = new RelayCommand(Open);
            SettingsOpen= new RelayCommand(Settings);
        }

        private void Open()
        {
            CurrentPage = TreeViewViewModel;
        }

        private void Settings()
        {
            CurrentPage = SettingsViewModel;
        }

    }
}
