using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel
{
    [DataContract(IsReference = true)]
    public abstract class BasePropertyModel
    {
        [DataMember] public virtual string Name { get; set; }
        public virtual BaseTypeModel Type { get; set; }
    }
}
