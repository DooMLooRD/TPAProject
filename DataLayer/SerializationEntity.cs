using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SerializationEntity
    {
        public int SerializationEntityId { get; set; }
        public byte[] SerializedObject { get; set; }
    }
}
