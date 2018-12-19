using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using MEF;

namespace FileData
{
    [Export(typeof(ILogger))]
    public class FileLogger : ILogger
    {
        public string FilePath { get; set; }
        public LogLevel SelectedLogLevel { get; set; }

        public FileLogger()
        {
            LoadPathFromConfig();
        }
        private void LoadPathFromConfig()
        {
            FilePath = ConfigurationManager.AppSettings["logFilename"] ?? "Log.log";
            string logLevel = ConfigurationManager.AppSettings["logLevel"] ?? "0";
            if (int.TryParse(logLevel, out int level))
            {
                SelectedLogLevel = (LogLevel)level;
            }
            else
            {
                SelectedLogLevel = LogLevel.Informative;
            }
        }
        public async void Log(MessageStructure message, LogCategoryEnum level)
        {
            if ((int)SelectedLogLevel > (int)level)
                return;

            using (TextWriter fileStream = new StreamWriter(File.Open(FilePath, FileMode.Append)))
            {
                string msg =
                    $"[{DateTime.Now}] "+$"{level}:".PadRight(13)+$" [{message.FileName}] in {message.OriginName}() line {message.LineNumber}: {message.Message}";
                await fileStream.WriteLineAsync(msg);
            }

        }

    }
}
