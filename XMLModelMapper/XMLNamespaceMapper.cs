using System.Linq;
using BusinessLogic.Model.Assembly;
using FileData.XMLModel;

namespace XMLModelMapper
{
    public class XMLNamespaceMapper
    {
        public NamespaceModel MapUp(XMLNamespaceModel model)
        {
            NamespaceModel namespaceModel = new NamespaceModel();
            namespaceModel.Name = model.Name;
            if (model.Types != null)
                namespaceModel.Types = model.Types.Select(n => XMLTypeMapper.EmitType((XMLTypeModel)n)).ToList();
            return namespaceModel;
        }

        public XMLNamespaceModel MapDown(NamespaceModel model)
        {
            XMLNamespaceModel namespaceModel = new XMLNamespaceModel();
            namespaceModel.Name = model.Name;
            if (model.Types != null)
                namespaceModel.Types = model.Types.Select(t=> new XMLTypeMapper().MapDown(t)).ToList();
            return namespaceModel;
        }
    }
}
