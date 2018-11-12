using System;
using System.IO;
using BusinessLogic.Logging;

namespace FileLogger
{
    public class FileLogger : ILogger
    {
        public string FilePath { get; set; }

        public FileLogger(string filePath)
        {
            FilePath = filePath;
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
