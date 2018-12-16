using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataLayer.DataModel;
using DataLayer.DataModel.Enums;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLTypeModel : BaseTypeModel
    {
        [DataMember] public override string Name { get; set; }
        [DataMember] public override string AssemblyName { get; set; }
        [DataMember] public override bool IsExternal { get; set; }
        [DataMember] public override bool IsGeneric { get; set; }

        [DataMember] public new XMLTypeModel BaseType { get; set; }
        [DataMember] public new List<XMLTypeModel> GenericArguments { get; set; }
        [DataMember] public override TypeModifiers Modifiers { get; set; }
        [DataMember] public override TypeEnum Type { get; set; }
        [DataMember] public new List<XMLTypeModel> ImplementedInterfaces { get; set; }
        [DataMember] public new List<XMLTypeModel> NestedTypes { get; set; }
        [DataMember] public new List<XMLPropertyModel> Properties { get; set; }
        [DataMember] public new XMLTypeModel DeclaringType { get; set; }
        [DataMember] public new List<XMLMethodModel> Methods { get; set; }
        [DataMember] public new List<XMLMethodModel> Constructors { get; set; }
        [DataMember] public new List<XMLParameterModel> Fields { get; set; }
    }
}
