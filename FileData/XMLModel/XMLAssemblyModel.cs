using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using BusinessLogic.Model.Assembly;
using MEF;

namespace FileData.XMLModel
{
    [DataContract]
    public class XMLAssemblyModel : IModelMapper<AssemblyModel,XMLAssemblyModel>
    {

        [DataMember] public string Name { get; set; }
        [DataMember] public List<XMLNamespaceModel> NamespaceModels { get; set; }


        public AssemblyModel MapUp(XMLAssemblyModel model)
        {
            AssemblyModel assemblyModel=new AssemblyModel();
            assemblyModel.Name = model.Name;
            if (model.NamespaceModels != null)
                assemblyModel.NamespaceModels = model.NamespaceModels.Select(n => n.MapUp(n)).ToList();
            return assemblyModel;

        }

        public XMLAssemblyModel MapDown(AssemblyModel model)
        {
            Name = model.Name;
            if (model.NamespaceModels != null)
                NamespaceModels = model.NamespaceModels.Select(n => new XMLNamespaceModel().MapDown(n)).ToList();
            return this;
        }
    }
}
