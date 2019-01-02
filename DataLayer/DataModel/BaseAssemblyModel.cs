using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DataLayer.DataModel
{
      
    public abstract class BaseAssemblyModel
    {
        public virtual string Name { get; set; }

        public virtual List<BaseNamespaceModel> NamespaceModels { get; set; }
    }
}
