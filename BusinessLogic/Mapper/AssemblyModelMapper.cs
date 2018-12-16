using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BusinessLogic.Model.Assembly;
using DataLayer.DataModel;

namespace BusinessLogic.Mapper
{
    public class AssemblyModelMapper 
    {

        public static AssemblyModel MapUp(BaseAssemblyModel model)
        {
            AssemblyModel assemblyModel = new AssemblyModel();
            Type type = model.GetType();
            assemblyModel.Name = model.Name;
            PropertyInfo namespaceModelsProperty = type.GetProperty("NamespaceModels",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            List<BaseNamespaceModel> namespaceModels= (List<BaseNamespaceModel>)HelperClass.ConvertList(typeof(BaseNamespaceModel),(IList)namespaceModelsProperty?.GetValue(model));
            if (namespaceModels != null)
                assemblyModel.NamespaceModels = namespaceModels.Select(n => new NamespaceModelMapper().MapUp(n)).ToList();
            return assemblyModel;
        }

        public static BaseAssemblyModel MapDown(AssemblyModel model, Type assemblyModelType)
        {
            object assemblyModel = Activator.CreateInstance(assemblyModelType);
            PropertyInfo nameProperty = assemblyModelType.GetProperty("Name");
            PropertyInfo namespaceModelsProperty = assemblyModelType.GetProperty("NamespaceModels",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            nameProperty?.SetValue(assemblyModel,model.Name);
            namespaceModelsProperty?.SetValue(
                assemblyModel,
                HelperClass.ConvertList(namespaceModelsProperty.PropertyType.GetGenericArguments()[0],
                    model.NamespaceModels.Select(n => new NamespaceModelMapper().MapDown(n,namespaceModelsProperty.PropertyType.GetGenericArguments()[0])).ToList()));
            return (BaseAssemblyModel)assemblyModel;
        }

    }
}
