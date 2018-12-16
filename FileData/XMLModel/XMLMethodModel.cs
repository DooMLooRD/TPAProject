using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataLayer.DataModel;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLMethodModel : BaseMethodModel
    {
        [DataMember] public override string Name { get; set; }

        [DataMember] public new List<XMLTypeModel> GenericArguments { get; set; }

        [DataMember] public new XMLTypeModel ReturnType { get; set; }

        [DataMember] public override bool Extension { get; set; }

        [DataMember] public new List<XMLParameterModel> Parameters { get; set; }

        [DataMember] public override MethodModifiers Modifiers { get; set; }
    }
}
