using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Logging
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
                    $"[{DateTime.Now}] "+$"{level}:".PadRight(11)+$" [{message.FileName}] in {message.OriginName}() line {message.LineNumber}: {message.Message}";
                await fileStream.WriteLineAsync(msg);
            }

        }
    }
}
