using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace BusinessLogic.Logging
{
    public class BaseLoggerFactory : ILogFactory
    {
        protected List<ILogger> Loggers = new List<ILogger>();

        protected object LoggerLock = new object();
        public LogLevel SelectedLogLevel { get; set; }
        
        public BaseLoggerFactory(LogLevel selectedLogLevel=LogLevel.Informative)
        {
            SelectedLogLevel = selectedLogLevel;
        }
        
        public BaseLoggerFactory(List<ILogger> loggers, LogLevel selectedLogLevel=LogLevel.Informative)
        {
            SelectedLogLevel = selectedLogLevel;
            Loggers = loggers;
        }

        public void AddLogger(ILogger logger)
        {
            lock (LoggerLock)
            {
                if (!Loggers.Contains(logger))
                    Loggers.Add(logger);
            }
        }

        public void RemoveLogger(ILogger logger)
        {
            lock (LoggerLock)
            {
                if (Loggers.Contains(logger))
                    Loggers.Remove(logger);
            }
        }

        public void Log(MessageStructure message, LogCategoryEnum level = LogCategoryEnum.Information)
        {
            if ((int)SelectedLogLevel > (int)level)
                return;

            lock(LoggerLock)
            {
                Loggers.ForEach(l => l.Log(message, level));
            }
            
        }
    }
}
