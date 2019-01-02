using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataLayer.DataModel;
using DataLayer.DataModel.Enums;

namespace FileData.XMLModel
{

    public class XMLTypeModel : BaseTypeModel
    {
        public override string Name { get; set; }
        public override string AssemblyName { get; set; }
        public override bool IsExternal { get; set; }
        public override bool IsGeneric { get; set; }
        public new XMLTypeModel BaseType { get; set; }
        public new List<XMLTypeModel> GenericArguments { get; set; }
        public override TypeModifiers Modifiers { get; set; }
        public override TypeEnum Type { get; set; }
        public new List<XMLTypeModel> ImplementedInterfaces { get; set; }
        public new List<XMLTypeModel> NestedTypes { get; set; }
        public new List<XMLPropertyModel> Properties { get; set; }
        public new XMLTypeModel DeclaringType { get; set; }
        public new List<XMLMethodModel> Methods { get; set; }
        public new List<XMLMethodModel> Constructors { get; set; }
        public new List<XMLParameterModel> Fields { get; set; }
    }
}
