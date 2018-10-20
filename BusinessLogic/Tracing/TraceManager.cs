using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Tracing
{
    public class TraceManager
    {
        private TraceListener _traceListener;

        public void Log(string message, LogCategoryEnum category)
        {
            _traceListener.WriteLine(message, category.ToString());
            _traceListener.Flush();
        }

        public void Log(string message)
        {
            _traceListener.WriteLine(message, LogCategoryEnum.Information.ToString());
            _traceListener.Flush();
        }

        public TraceManager(TraceListener traceListener)
        {
            _traceListener = traceListener;
        }
    }
}
