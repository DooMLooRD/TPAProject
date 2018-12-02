using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using BusinessLogic.Model.Assembly;
using MEF;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLNamespaceModel :IModelMapper<NamespaceModel, XMLNamespaceModel>       
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public List<XMLTypeModel> Types { get; set; }
        public NamespaceModel MapUp(XMLNamespaceModel model)
        {
            NamespaceModel namespaceModel=new NamespaceModel();
            namespaceModel.Name = model.Name;
            if (model.Types != null)
                namespaceModel.Types = model.Types.Select(XMLTypeModel.GetType).ToList();
            return namespaceModel;
        }

        public XMLNamespaceModel MapDown(NamespaceModel model)
        {
            Name = model.Name;
            if (model.Types != null)
                Types = model.Types.Select(XMLTypeModel.GetXMLType).ToList();
            return this;
        }
    }
}
