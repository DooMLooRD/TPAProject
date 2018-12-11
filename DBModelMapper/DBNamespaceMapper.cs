using System.Linq;
using BusinessLogic.Model.Assembly;
using DBData.Entities;

namespace DBModelMapper
{
    public class DBNamespaceMapper
    {
        public NamespaceModel MapUp(DBNamespaceModel model)
        {
            NamespaceModel namespaceModel = new NamespaceModel();
            namespaceModel.Name = model.Name;
            if (model.Types != null)
                namespaceModel.Types = model.Types.Select(n => DBTypeMapper.EmitType((DBTypeModel)n)).ToList();
            return namespaceModel;
        }

        public DBNamespaceModel MapDown(NamespaceModel model)
        {
            DBNamespaceModel namespaceModel = new DBNamespaceModel();
            namespaceModel.Name = model.Name;
            if (model.Types != null)
                namespaceModel.Types = model.Types.Select(t=> new DBTypeMapper().MapDown(t)).ToList();
            return namespaceModel;
        }
    }
}
