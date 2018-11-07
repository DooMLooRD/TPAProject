using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Logging;

namespace BusinessLogic.Logging
{
    public interface ILogFactory
    {
        void AddLogger(ILogger logger);
        void RemoveLogger(ILogger logger);
        void Log(MessageStructure message, LogCategoryEnum level = LogCategoryEnum.Information);

    }
}
