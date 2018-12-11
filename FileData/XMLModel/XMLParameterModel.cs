using System.Runtime.Serialization;
using DataLayer.DataModel;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLParameterModel 
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public XMLTypeModel Type { get; set; }

    }
}
