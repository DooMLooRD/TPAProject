using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel
{

    public abstract class BaseNamespaceModel
    {
        public virtual string Name { get; set; }
        public virtual List<BaseTypeModel> Types { get; set; }
    }
}
