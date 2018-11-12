using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLogic.Logging
{
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
