using BusinessLogic.Model.Assembly;
using FileData.XMLModel;

namespace XMLModelMapper
{
    public class XMLPropertyMapper
    {
        public PropertyModel MapUp(XMLPropertyModel model)
        {
            PropertyModel propertyModel = new PropertyModel();
            propertyModel.Name = model.Name;
            if (model.Type != null)
                propertyModel.Type =XMLTypeMapper.EmitType((XMLTypeModel)model.Type);
            return propertyModel;
        }

        public XMLPropertyModel MapDown(PropertyModel model)
        {
            XMLPropertyModel propertyModel = new XMLPropertyModel();
            propertyModel.Name = model.Name;
            if (model.Type != null)
                propertyModel.Type =XMLTypeMapper.EmitXMLType(model.Type);
            return propertyModel;
        }
    }
}
