using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel
{

    public abstract class BaseParameterModel
    {
        public virtual string Name { get; set; }
        public virtual BaseTypeModel Type { get; set; }
    }
}
