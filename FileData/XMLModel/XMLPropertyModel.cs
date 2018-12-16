using System.Runtime.Serialization;
using DataLayer.DataModel;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLPropertyModel : BasePropertyModel
    {

        [DataMember] public override string Name { get; set; }

        [DataMember] public new XMLTypeModel Type { get; set; }

    }
}
