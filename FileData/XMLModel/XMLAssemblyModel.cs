using System.Collections.Generic;
using System.Runtime.Serialization;
using DataLayer.DataModel;

namespace FileData.XMLModel
{
    [DataContract]
    public class XMLAssemblyModel : IAssemblyModel
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public List<XMLNamespaceModel> NamespaceModels { get; set; }
    }
}
