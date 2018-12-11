using BusinessLogic.Model.Assembly;
using DBData.Entities;

namespace DBModelMapper
{
    public class DBPropertyMapper
    {
        public PropertyModel MapUp(DBPropertyModel model)
        {
            PropertyModel propertyModel = new PropertyModel();
            propertyModel.Name = model.Name;
            if (model.Type != null)
                propertyModel.Type =DBTypeMapper.EmitType((DBTypeModel)model.Type);
            return propertyModel;
        }

        public DBPropertyModel MapDown(PropertyModel model)
        {
            DBPropertyModel propertyModel = new DBPropertyModel();
            propertyModel.Name = model.Name;
            if (model.Type != null)
                propertyModel.Type =DBTypeMapper.EmitDBType(model.Type);
            return propertyModel;
        }
    }
}
