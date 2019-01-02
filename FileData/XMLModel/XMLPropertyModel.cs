using System.Runtime.Serialization;
using DataLayer.DataModel;

namespace FileData.XMLModel
{
    public class XMLPropertyModel : BasePropertyModel
    {

         public override string Name { get; set; }
         public new XMLTypeModel Type { get; set; }

    }
}
