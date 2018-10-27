using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Logging;

namespace BusinessLogic.DI.Interfaces
{
    public interface ILogger
    {
        void Log(MessageStructure message, LogCategoryEnum level);
    }
}
