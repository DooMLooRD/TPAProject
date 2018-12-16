using System.Runtime.Serialization;
using DataLayer.DataModel;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLParameterModel : BaseParameterModel
    {

        [DataMember] public override string Name { get; set; }

        [DataMember] public new XMLTypeModel Type { get; set; }

    }
}
