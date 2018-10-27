using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Logging;

namespace BusinessLogic.DI.Interfaces
{
    public interface ILogFactory
    {
        LogLevel SelectedLogLevel { get; set; }

        void AddLogger(ILogger logger);
        void RemoveLogger(ILogger logger);

        void Log(MessageStructure message, LogCategoryEnum level = LogCategoryEnum.Information);
    }
}
