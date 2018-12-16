using System;
using System.Reflection;
using BusinessLogic.Model.Assembly;
using DataLayer.DataModel;

namespace BusinessLogic.Mapper
{
    public class PropertyModelMapper
    {
        public PropertyModel MapUp(BasePropertyModel model)
        {
            PropertyModel propertyModel = new PropertyModel();
            propertyModel.Name = model.Name;
            Type type = model.GetType();
            PropertyInfo typeProperty = type.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseTypeModel typeModel = (BaseTypeModel)typeProperty?.GetValue(model);

            if (typeModel != null)
                propertyModel.Type = TypeModelMapper.EmitType(typeModel);

            return propertyModel;
        }

        public BasePropertyModel MapDown(PropertyModel model,Type propertyModelType)
        {
            object propertyModel = Activator.CreateInstance(propertyModelType);
            PropertyInfo nameProperty = propertyModelType.GetProperty("Name");
            PropertyInfo typeProperty = propertyModelType.GetProperty("Type", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(propertyModel, model.Name);

            if (model.Type != null)
                typeProperty?.SetValue(propertyModel, 
                    typeProperty.PropertyType.Cast(TypeModelMapper.EmitBaseType(model.Type, typeProperty.PropertyType)));

            return (BasePropertyModel)propertyModel;
        }
    }
}
