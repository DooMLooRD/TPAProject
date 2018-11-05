using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class TPADataContext :DbContext
    {
        public DbSet<LogEntity> LogEntities { get; set; }
        public  DbSet<SerializationEntity> SerializationEntities { get; set; }
    }
}
