using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using DataLayer.DataModel;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    [Export(typeof(BaseAssemblyModel))]
    public class XMLAssemblyModel : BaseAssemblyModel
    {

        [DataMember] public override string Name { get; set; }


        [DataMember] public new List<XMLNamespaceModel> NamespaceModels { get; set; }

    }
}
