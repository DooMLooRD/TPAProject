using System.Collections.Generic;
using System.Runtime.Serialization;
using DataLayer.DataModel;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLMethodModel 
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public List<XMLTypeModel> GenericArguments { get; set; }
        [DataMember] public MethodModifiers Modifiers { get; set; }
        [DataMember] public XMLTypeModel ReturnType { get; set; }
        [DataMember] public bool Extension { get; set; }
        [DataMember] public List<XMLParameterModel> Parameters { get; set; }
    }
}
