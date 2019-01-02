using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataModel
{
    
    public abstract class BaseMethodModel
    {
        public virtual string Name { get; set; }
        public virtual List<BaseTypeModel> GenericArguments { get; set; }
        public virtual MethodModifiers Modifiers { get; set; }
        public virtual BaseTypeModel ReturnType { get; set; }
        public virtual bool Extension { get; set; }
        public virtual List<BaseParameterModel> Parameters { get; set; }
    }
}
