using System.Runtime.Serialization;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLPropertyModel 
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public XMLTypeModel Type { get; set; }
    }
}
