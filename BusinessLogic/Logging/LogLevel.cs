using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logging
{
    public enum LogLevel
    {
        /// <summary>
        /// The highest level of logging (logs everything)
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Log all informations except debug messages
        /// </summary>
        Informative = 2,

        /// <summary>
        /// Logs errors, warnings, and normal messages
        /// </summary>
        Normal = 3,

        /// <summary>
        /// Logs errors and warnings only
        /// </summary>
        Critical = 5,

        /// <summary>
        /// Logs nothing
        /// </summary>
        Nothing = 6
    }
}
