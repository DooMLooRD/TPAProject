using System.ComponentModel.Composition;
using System.Linq;
using BusinessLogic;
using BusinessLogic.Model.Assembly;
using DataLayer.DataModel;
using FileData.XMLModel;

namespace XMLModelMapper
{
    [Export(typeof(IMapper))]
    public class XMLAssemblyMapper : IMapper
    {

        public AssemblyModel MapUp(IAssemblyModel model)
        {
            AssemblyModel assemblyModel = new AssemblyModel();
            assemblyModel.Name = model.Name;
            if (((XMLAssemblyModel)model).NamespaceModels != null)
                assemblyModel.NamespaceModels = ((XMLAssemblyModel)model).NamespaceModels.Select(n => new XMLNamespaceMapper().MapUp(n)).ToList();
            return assemblyModel;
        }

        public IAssemblyModel MapDown(AssemblyModel model)
        {
            XMLAssemblyModel assemblyModel = new XMLAssemblyModel();
            assemblyModel.Name = model.Name;
            if (model.NamespaceModels != null)
                assemblyModel.NamespaceModels = model.NamespaceModels.Select(n => new XMLNamespaceMapper().MapDown(n)).ToList();
            return assemblyModel;
        }

    }
}
