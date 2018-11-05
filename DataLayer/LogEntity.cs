using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LogEntity
    {
        public int LogEntityId { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public string OriginName { get; set; }
        public string FileName { get; set; }
        public int Line { get; set; }
        public string LogCategory { get; set; }
        
    }
}
