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
                    $"[{DateTime.Now}] "+$"{level}:".PadRight(13)+$" [{message.FileName}] in {message.OriginName}() line {message.LineNumber}: {message.Message}";
                await fileStream.WriteLineAsync(msg);
            }

        }

        public override bool Equals(object obj)
        {
            if (this.GetType() == obj?.GetType())
            {
                if (FilePath.Equals(((FileLogger) obj).FilePath))
                    return true;
            }
            return false;
        }

        protected bool Equals(FileLogger other)
        {
            return string.Equals(FilePath, other.FilePath);
        }

        public override int GetHashCode()
        {
            return (FilePath != null ? FilePath.GetHashCode() : 0);
        }
    }
}
