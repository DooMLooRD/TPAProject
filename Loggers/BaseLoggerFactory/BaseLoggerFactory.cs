using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using MEF;

namespace BaseLoggerFactory
{
    [Export(typeof(ILogFactory))]
    public class BaseLoggerFactory : ILogFactory
    {
        #region Fields and properties

        private bool _isInitated;
        protected List<ILogger> Loggers = new List<ILogger>();
        [ImportMany(typeof(ILogger))]
        public IEnumerable<Lazy<ILogger, IDictionary<string, object>>> loggers;
        protected object LoggerLock = new object();
        public LogLevel SelectedLogLevel { get; set; }

        #endregion

        #region Constructors
        public BaseLoggerFactory(LogLevel selectedLogLevel)
        {
            SelectedLogLevel = selectedLogLevel;
        }
        public BaseLoggerFactory() : this(LogLevel.Informative)
        {
        }

        #endregion

        #region Public methods

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
            if (!_isInitated)
            {
                InitLoggers();
                _isInitated = true;
            }

            if ((int)SelectedLogLevel > (int)level)
                return;

            lock (LoggerLock)
            {
                Loggers.ForEach(l => l.Log(message, level));
            }

        }

        #endregion

        #region MEF



        public void InitLoggers()
        {
            string[] loggerType = ConfigurationManager.AppSettings["loggerTypes"].Split(',');
            lock (LoggerLock)
            {
                Loggers = loggers.Where(t => loggerType.Contains((string)t.Metadata["Logger"])).Select(T => T.Value)
                    .ToList();
            }

            string logLevel = ConfigurationManager.AppSettings["logLevel"];

            if (int.TryParse(logLevel, out int level))
            {
                SelectedLogLevel = (LogLevel)level;
            }
            else
            {
                SelectedLogLevel = LogLevel.Informative;
            }
        }

        #endregion
    }
}
