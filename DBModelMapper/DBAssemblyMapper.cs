using System.ComponentModel.Composition;
using System.Linq;
using BusinessLogic;
using BusinessLogic.Model.Assembly;
using DataLayer.DataModel;
using DBData.Entities;

namespace DBModelMapper
{
    [Export(typeof(IMapper))]
    public class DBAssemblyMapper : IMapper
    {
        public AssemblyModel MapUp(IAssemblyModel model)
        {
            AssemblyModel assemblyModel = new AssemblyModel();
            assemblyModel.Name = model.Name;
            if (((DBAssemblyModel)model).NamespaceModels != null)
                assemblyModel.NamespaceModels = ((DBAssemblyModel)model).NamespaceModels.Select(n => new DBNamespaceMapper().MapUp(n)).ToList();
            return assemblyModel;
        }

        public IAssemblyModel MapDown(AssemblyModel model)
        {
            DBAssemblyModel assemblyModel = new DBAssemblyModel();
            assemblyModel.Name = model.Name;
            if (model.NamespaceModels != null)
                assemblyModel.NamespaceModels = model.NamespaceModels.Select(n => new DBNamespaceMapper().MapDown(n)).ToList();
            return assemblyModel;
        }

    }
}
