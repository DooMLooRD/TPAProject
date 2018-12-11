using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLNamespaceModel 
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public List<XMLTypeModel> Types { get; set; }

    }
}
