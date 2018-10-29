using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogic.Logging
{
    public interface ILogger
    {
        void Log(MessageStructure message, LogCategoryEnum level);
    }
}
