using System;
using System.ComponentModel.Composition;
using DBData.Entities;
using MEF;

namespace DBData
{
    [Export(typeof(ILogger))]
    public class DatabaseLogger : ILogger
    {
        public void Log(MessageStructure message, LogCategoryEnum level)
        {
            using (TPADBContext context=new TPADBContext())
            {
                context.Log.Add(new LogEntity
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
}
