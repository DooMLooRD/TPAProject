using System;
using System.ComponentModel.Composition;
using BusinessLogic.Logging;
using DataLayer;

namespace DatabaseLogger
{
    [Export(typeof(ILogger))]
    [ExportMetadata("Logger", "Database")]
    public class DatabaseLogger : ILogger
    {
        Repository repository = new Repository();
        public void Log(MessageStructure message, LogCategoryEnum level)
        {
            repository.Log(new LogEntity()
            {
                Message = message.Message,
                FileName = message.FileName,
                Line = message.LineNumber,
                OriginName = message.OriginName,
                LogCategory = level.ToString(),
                Time = DateTime.Now
            });
        }

    }
}
