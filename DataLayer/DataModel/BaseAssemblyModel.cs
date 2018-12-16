using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DataLayer.DataModel
{
    [DataContract(IsReference = true)]
    public abstract class BaseAssemblyModel
    {
        [DataMember] public virtual string Name { get; set; }

        public virtual List<BaseNamespaceModel> NamespaceModels { get; set; }
    }
}
