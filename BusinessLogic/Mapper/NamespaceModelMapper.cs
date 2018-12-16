using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BusinessLogic.Model.Assembly;
using DataLayer.DataModel;

namespace BusinessLogic.Mapper
{
    public class NamespaceModelMapper
    {
        public NamespaceModel MapUp(BaseNamespaceModel model)
        {
            NamespaceModel namespaceModel = new NamespaceModel();
            namespaceModel.Name = model.Name;
            Type type = model.GetType();
            PropertyInfo typesProperty = type.GetProperty("Types",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            List<BaseTypeModel> types = (List<BaseTypeModel>)HelperClass.ConvertList(typeof(BaseTypeModel), (IList)typesProperty?.GetValue(model));
            if (types != null)
                namespaceModel.Types = types.Select(n => TypeModelMapper.EmitType(n)).ToList();
            return namespaceModel;
        }

        public BaseNamespaceModel MapDown(NamespaceModel model, Type namespaceModelType)
        {
            object namespaceModel = Activator.CreateInstance(namespaceModelType);
            PropertyInfo nameProperty = namespaceModelType.GetProperty("Name");
            PropertyInfo namespaceModelsProperty = namespaceModelType.GetProperty("Types",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(namespaceModel, model.Name);
            namespaceModelsProperty?.SetValue(namespaceModel,
                HelperClass.ConvertList(namespaceModelsProperty.PropertyType.GetGenericArguments()[0],
                    model.Types.Select(t => new TypeModelMapper().MapDown(t, namespaceModelsProperty.PropertyType.GetGenericArguments()[0])).ToList()));

            return (BaseNamespaceModel)namespaceModel;
        }
    }
}
