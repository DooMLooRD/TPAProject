using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logging
{
    public struct MessageStructure
    {
        public string Message { get; set; }
        public string OriginName { get; set; }
        public string FileName { get; set; }
        public int LineNumber { get; set; }

        public MessageStructure(string message, [CallerMemberName] string originName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Message = message;
            OriginName = originName;
            FileName = fileName;
            LineNumber = lineNumber;
        }

    }
}
