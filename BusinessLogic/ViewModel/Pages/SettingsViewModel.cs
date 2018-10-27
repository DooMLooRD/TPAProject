using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLogic.Logging;

namespace BusinessLogic.ViewModel.Pages
{
    public class SettingsViewModel : BaseViewModel
    {
        public LogLevel LogLevel { get; set; }
        public ICommand SaveCommand { get; }

        public SettingsViewModel()
        {
            SaveCommand=new RelayCommand(Save);
        }

        private void Save()
        {
            using (TextWriter fileStream = new StreamWriter(File.Open("settings.config", FileMode.Append)))
            {
                fileStream.WriteLine(LogLevel);
            }
        }
    }
}
