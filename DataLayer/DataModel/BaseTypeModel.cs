using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModel.Enums;

namespace DataLayer.DataModel
{
    [DataContract(IsReference = true)]
    public abstract class BaseTypeModel
    {
        [DataMember] public virtual string Name { get; set; }
        [DataMember] public virtual string AssemblyName { get; set; }
        [DataMember] public virtual bool IsExternal { get; set; }
        [DataMember] public virtual bool IsGeneric { get; set; }
        public virtual BaseTypeModel BaseType { get; set; }
        public virtual List<BaseTypeModel> GenericArguments { get; set; }
        [DataMember] public virtual TypeModifiers Modifiers { get; set; }
        [DataMember] public virtual TypeEnum Type { get; set; }
        public virtual List<BaseTypeModel> ImplementedInterfaces { get; set; }
        public virtual List<BaseTypeModel> NestedTypes { get; set; }
        public virtual List<BasePropertyModel> Properties { get; set; }
        public virtual BaseTypeModel DeclaringType { get; set; }
        public virtual List<BaseMethodModel> Methods { get; set; }
        public virtual List<BaseMethodModel> Constructors { get; set; }
        public virtual List<BaseParameterModel> Fields { get; set; }

    }
}
