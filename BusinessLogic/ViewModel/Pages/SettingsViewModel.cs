using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLogic.DI.Base;
using BusinessLogic.Logging;
using BusinessLogic.Serialization;

namespace BusinessLogic.ViewModel.Pages
{
    [DataContract]
    public class SettingsViewModel : BaseViewModel
    {

        #region Backing Field

        private LogLevel _logLevel;
        private string _loggerConnectionString;
        private string _loggerFilePath;
        private ISerializer _serializer;
        private string _serializerFilePath;
        private string _serializerConnectionString;
        private bool _isFileLoggerChecked;
        private bool _isDbLoggerChecked;

        #endregion

        #region Property
        [DataMember]
        public bool IsFileLoggerChecked
        {
            get => _isFileLoggerChecked;
            set
            {
                _isFileLoggerChecked = value;
                OnPropertyChanged(nameof(IsFileLoggerChecked));
            }
        }

        [DataMember]
        public bool IsDbLoggerChecked
        {
            get => _isDbLoggerChecked;
            set
            {
                _isDbLoggerChecked = value;
                OnPropertyChanged(nameof(IsDbLoggerChecked));
            }
        }

        [DataMember]
        public LogLevel LogLevel
        {
            get => _logLevel;
            set
            {
                _logLevel = value;
                OnPropertyChanged(nameof(LogLevel));
            }
        }
        [DataMember]
        public string LoggerConnectionString
        {
            get => _loggerConnectionString;
            set
            {
                _loggerConnectionString = value;
                OnPropertyChanged(nameof(LoggerConnectionString));
            }
        }
        [DataMember]
        public string SerializerConnectionString
        {
            get => _serializerConnectionString;
            set
            {
                _serializerConnectionString = value;
                OnPropertyChanged(nameof(SerializerConnectionString));
            }
        }
        [DataMember]
        public string LoggerFilePath
        {
            get => _loggerFilePath;
            set
            {
                _loggerFilePath = value;
                OnPropertyChanged(nameof(LoggerFilePath));
            }

        }
        [DataMember]
        public string SerializerFilePath
        {
            get => _serializerFilePath;
            set
            {
                _serializerFilePath = value;
                OnPropertyChanged(nameof(SerializerFilePath));
            }

        }
        [DataMember]
        public ISerializer Serializer
        {
            get => _serializer;
            set
            {
                _serializer = value;
                OnPropertyChanged(nameof(Serializer));
            }
        }

        #endregion

        #region Commands

        public ICommand SaveCommand { get; }

        #endregion

        #region Constructors

        public SettingsViewModel()
        {
            SaveCommand = new RelayCommand(Save);
        }

        #endregion

        #region Methods

        private void Save()
        {

            XMLSerializer serializer = new XMLSerializer();
            serializer.Serialize(this, "settings.xml");


        }

        public void LoadSettings()
        {
            if (File.Exists("settings.xml"))
            {
                XMLSerializer serializer = new XMLSerializer();
                SettingsViewModel vm = serializer.Deserialize<SettingsViewModel>("settings.xml");
                IsFileLoggerChecked = vm.IsFileLoggerChecked;
                IsDbLoggerChecked = vm.IsDbLoggerChecked;
                LogLevel = vm.LogLevel;
                LoggerConnectionString = vm.LoggerConnectionString;
                LoggerFilePath = vm.LoggerFilePath;
                //Need to add db serializer
                Serializer = new XMLSerializer();
                SerializerConnectionString = vm.SerializerConnectionString;
                SerializerFilePath = vm.SerializerFilePath;
                if (IsFileLoggerChecked)
                {
                    IoC.Get<ILogFactory>().AddLogger(new FileLogger(LoggerFilePath));
                }
                if (IsDbLoggerChecked)
                {
                    IoC.Get<ILogFactory>().AddLogger(new DatabaseLogger());
                }

                IoC.Get<TreeViewViewModel>().Serializer = Serializer;
                IoC.Get<TreeViewViewModel>().SerializePath = SerializerFilePath;
            }
        }

        #endregion

    }
}
