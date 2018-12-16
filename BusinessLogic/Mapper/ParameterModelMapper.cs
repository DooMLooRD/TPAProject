using System;
using System.Reflection;
using BusinessLogic.Model.Assembly;
using DataLayer.DataModel;

namespace BusinessLogic.Mapper
{
    public class ParameterModelMapper
    {
        public ParameterModel MapUp(BaseParameterModel model)
        {
            ParameterModel parameterModel = new ParameterModel();
            parameterModel.Name = model.Name;
            Type type = model.GetType();
            PropertyInfo typeProperty = type.GetProperty("Type", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseTypeModel typeModel = (BaseTypeModel)typeProperty?.GetValue(model);
            if (typeModel != null)
                parameterModel.Type = TypeModelMapper.EmitType(typeModel);
            return parameterModel;
        }

        public BaseParameterModel MapDown(ParameterModel model, Type parameterModelType)
        {
            object parameterModel = Activator.CreateInstance(parameterModelType);
            PropertyInfo nameProperty = parameterModelType.GetProperty("Name");
            PropertyInfo typeProperty = parameterModelType.GetProperty("Type",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(parameterModel, model.Name);
            if (model.Type != null)
                typeProperty?.SetValue(parameterModel, 
                    typeProperty.PropertyType.Cast(TypeModelMapper.EmitBaseType(model.Type, typeProperty.PropertyType)));

            return (BaseParameterModel)parameterModel;
        }
    }
}
