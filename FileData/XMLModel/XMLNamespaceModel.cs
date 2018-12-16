using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataLayer.DataModel;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLNamespaceModel : BaseNamespaceModel
    {

        [DataMember] public override string Name { get; set; }

        [DataMember] public new List<XMLTypeModel> Types { get; set; }

    }
}
