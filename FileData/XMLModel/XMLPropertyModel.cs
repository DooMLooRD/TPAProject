using System.Runtime.Serialization;
using BusinessLogic.Model.Assembly;
using MEF;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLPropertyModel : IModelMapper<PropertyModel, XMLPropertyModel>
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public XMLTypeModel Type { get; set; }

        public PropertyModel MapUp(XMLPropertyModel model)
        {
            PropertyModel propertyModel = new PropertyModel();
            propertyModel.Name = model.Name;
            if (model.Type != null)
                propertyModel.Type = XMLTypeModel.GetType(model.Type);
            return propertyModel;
        }

        public XMLPropertyModel MapDown(PropertyModel model)
        {
            Name = model.Name;
            if (model.Type != null)
                Type = XMLTypeModel.GetXMLType(model.Type);
            return this;
        }
    }
}
