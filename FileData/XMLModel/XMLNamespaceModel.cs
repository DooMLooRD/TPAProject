using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataLayer.DataModel;

namespace FileData.XMLModel
{
    
    public class XMLNamespaceModel : BaseNamespaceModel
    {

         public override string Name { get; set; }

         public new List<XMLTypeModel> Types { get; set; }

    }
}
