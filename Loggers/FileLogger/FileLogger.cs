using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using BusinessLogic.Logging;

namespace FileLogger
{
    [Export(typeof(ILogger))]
    [ExportMetadata("Logger", "File")]
    public class FileLogger : ILogger
    {
        public string FilePath { get; set; }

        public FileLogger()
        {
            LoadPathFromConfig();
        }
        private void LoadPathFromConfig()
        {
            FilePath = ConfigurationManager.AppSettings["filename"] ?? "Log.log";
        }
        public async void Log(MessageStructure message, LogCategoryEnum level)
        {

            using (TextWriter fileStream = new StreamWriter(File.Open(FilePath, FileMode.Append)))
            {
                string msg =
                    $"[{DateTime.Now}] "+$"{level}:".PadRight(13)+$" [{message.FileName}] in {message.OriginName}() line {message.LineNumber}: {message.Message}";
                await fileStream.WriteLineAsync(msg);
            }

        }

    }
}
