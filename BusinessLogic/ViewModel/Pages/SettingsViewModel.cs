using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ninject;

namespace BusinessLogic.ViewModel.Pages
{
    [DataContract]
    public class SettingsViewModel : BaseViewModel
    {
        private Settings.Settings _settings;

        [Inject]
        public Settings.Settings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                OnPropertyChanged(nameof(Settings));
            }
        }

        #region Commands

        public ICommand SaveCommand { get; }

        #endregion

        #region Constructors

        public SettingsViewModel()
        {
            SaveCommand = new RelayCommand(Save);
            //Settings=new Settings.Settings();
        }

        #endregion

        #region Methods

        private void Save()
        {
            Settings.Save();
        }

        #endregion

    }
}
