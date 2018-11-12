using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace BusinessLogic.Logging
{
    public class BaseLoggerFactory : ILogFactory
    {
        #region Fields and properties

        protected List<ILogger> Loggers = new List<ILogger>();

        protected object LoggerLock = new object();
        public LogLevel SelectedLogLevel { get; set; }

        #endregion

        #region Constructors
        public BaseLoggerFactory(LogLevel selectedLogLevel = LogLevel.Informative)
        {
            SelectedLogLevel = selectedLogLevel;
            Compose();
            InitLogger();
        }

        public BaseLoggerFactory(List<ILogger> loggers, LogLevel selectedLogLevel = LogLevel.Informative) : this(selectedLogLevel)
        {
            Loggers = loggers;
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
            if ((int)SelectedLogLevel > (int)level)
                return;

            lock (LoggerLock)
            {
                Loggers.ForEach(l => l.Log(message, level));
            }

        }

        #endregion

        #region MEF

        [ImportMany(typeof(ILogger))]
        public IEnumerable<Lazy<ILogger, IDictionary<string, object>>> MefLoggers;

        public void Compose()
        {
            NameValueCollection plugins = (NameValueCollection)ConfigurationManager.GetSection("plugins");
            string[] pluginsCatalogs = plugins.AllKeys;
            List<DirectoryCatalog> directoryCatalogs = new List<DirectoryCatalog>();
            foreach (string pluginsCatalog in pluginsCatalogs)
            {
                if (Directory.Exists(pluginsCatalog))
                    directoryCatalogs.Add(new DirectoryCatalog(pluginsCatalog));
            }

            AggregateCatalog catalog = new AggregateCatalog(directoryCatalogs);
            CompositionContainer container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
            catch (Exception exception) when (exception is ReflectionTypeLoadException)
            {
                ReflectionTypeLoadException typeLoadException = (ReflectionTypeLoadException)exception;
                Exception[] loaderExceptions = typeLoadException.LoaderExceptions;
                loaderExceptions.ToList().ForEach(ex => Console.WriteLine(ex.StackTrace));

                throw;
            }


        }

        public void InitLogger()
        {
            string[] loggerType = ConfigurationManager.AppSettings["loggerTypes"].Split(',');
            lock (LoggerLock)
            {
                Loggers = MefLoggers.Where(t => loggerType.Contains((string)t.Metadata["Logger"])).Select(T => T.Value)
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
