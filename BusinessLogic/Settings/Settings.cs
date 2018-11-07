using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Logging;
using BusinessLogic.Serialization;
using BusinessLogic.Settings.Helper;

namespace BusinessLogic.Settings
{
    [DataContract]
    public class Settings : INotifyPropertyChanged
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

        public Settings()
        {
            LoadSettings();
        }
        #endregion

        #region Methods

        public void Save()
        {

            SettingsSerializer serializer = new SettingsSerializer();
            serializer.Serialize(this, "settings.config");


        }

        public async void LoadSettings()
        {
            if (File.Exists("settings.config"))
            {
                SettingsSerializer serializer = new SettingsSerializer();
                Settings settings = await serializer.Deserialize<Settings>("settings.config");
                IsFileLoggerChecked = settings.IsFileLoggerChecked;
                IsDbLoggerChecked = settings.IsDbLoggerChecked;
                LogLevel = settings.LogLevel;
                LoggerConnectionString = settings.LoggerConnectionString;
                LoggerFilePath = settings.LoggerFilePath;
                Serializer = new XMLSerializer();
                SerializerConnectionString = settings.SerializerConnectionString;
                SerializerFilePath = settings.SerializerFilePath;
                
            }
            else
            {
                IsFileLoggerChecked = true;
                IsDbLoggerChecked = false;
                LogLevel = LogLevel.Informative;
                LoggerConnectionString = @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=DataLayer.Model1;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                LoggerFilePath = "Log.txt";
                Serializer = new XMLSerializer();
                SerializerConnectionString = @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=DataLayer.Model1;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
                SerializerFilePath = "Serialized.xml";
                Save();
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        public void OnPropertyChanged(string name)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
