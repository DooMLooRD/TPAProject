using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BusinessLogic.Model.Assembly;
using DataLayer.DataModel;

namespace BusinessLogic.Mapper
{
    public class TypeModelMapper
    {
        public static Dictionary<string, BaseTypeModel> BaseTypes = new Dictionary<string, BaseTypeModel>();
        public static Dictionary<string, TypeModel> Types = new Dictionary<string, TypeModel>();

        public static BaseTypeModel EmitBaseType(TypeModel model, Type type)
        {
            return new TypeModelMapper().MapDown(model, type);
        }

        public static TypeModel EmitType(BaseTypeModel model)
        {
            return new TypeModelMapper().MapUp(model);
        }

        private void FillBaseType(TypeModel model, BaseTypeModel typModel)
        {
            Type typeModelType = typModel.GetType();

            typeModelType.GetProperty("Name")?.SetValue(typModel, model.Name);
            typeModelType.GetProperty("IsExternal")?.SetValue(typModel, model.IsExternal);
            typeModelType.GetProperty("IsGeneric")?.SetValue(typModel, model.IsGeneric);
            typeModelType.GetProperty("Type")?.SetValue(typModel, model.Type);
            typeModelType.GetProperty("AssemblyName")?.SetValue(typModel, model.AssemblyName);
            typeModelType.GetProperty("Modifiers")?.SetValue(typModel, model.Modifiers ?? new TypeModifiers());

            if (model.BaseType != null)
            {
                typeModelType.GetProperty("BaseType",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    ?.SetValue(typModel, typeModelType.Cast(EmitBaseType(model.BaseType, typeModelType)));
            }

            if (model.DeclaringType != null)
            {
                typeModelType.GetProperty("DeclaringType",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    ?.SetValue(typModel, typeModelType.Cast(EmitBaseType(model.DeclaringType, typeModelType)));
            }

            if (model.NestedTypes != null)
            {
                PropertyInfo nestedTypesProperty = typeModelType.GetProperty("NestedTypes",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                nestedTypesProperty?.SetValue(typModel,
                    HelperClass.ConvertList(typeModelType,
                        model.NestedTypes?.Select(c => EmitBaseType(c, typeModelType)).ToList()));
            }

            if (model.GenericArguments != null)
            {
                PropertyInfo genericArgumentsProperty = typeModelType.GetProperty("GenericArguments",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                genericArgumentsProperty?.SetValue(typModel,
                    HelperClass.ConvertList(typeModelType,
                        model.GenericArguments?.Select(c => EmitBaseType(c, typeModelType)).ToList()));
            }

            if (model.ImplementedInterfaces != null)
            {
                PropertyInfo implementedInterfacesProperty = typeModelType.GetProperty("ImplementedInterfaces",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                implementedInterfacesProperty?.SetValue(typModel,
                    HelperClass.ConvertList(typeModelType,
                        model.ImplementedInterfaces?.Select(c => EmitBaseType(c, typeModelType)).ToList()));
            }

            if (model.Fields != null)
            {
                PropertyInfo fieldsProperty = typeModelType.GetProperty("Fields",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                fieldsProperty?.SetValue(typModel,
                    HelperClass.ConvertList(fieldsProperty.PropertyType.GetGenericArguments()[0],
                        model.Fields?.Select(c =>
                            new ParameterModelMapper().MapDown(c,
                                fieldsProperty?.PropertyType.GetGenericArguments()[0])).ToList()));
            }

            if (model.Methods != null)
            {
                PropertyInfo methodsProperty = typeModelType.GetProperty("Methods",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                methodsProperty?.SetValue(typModel,
                    HelperClass.ConvertList(methodsProperty.PropertyType.GetGenericArguments()[0],
                        model.Methods?.Select(m =>
                                new MethodModelMapper().MapDown(m,
                                    methodsProperty?.PropertyType.GetGenericArguments()[0]))
                            .ToList()));
            }

            if (model.Constructors != null)
            {
                PropertyInfo constructorsProperty = typeModelType.GetProperty("Constructors",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                constructorsProperty?.SetValue(typModel,
                    HelperClass.ConvertList(constructorsProperty.PropertyType.GetGenericArguments()[0],
                        model.Constructors?.Select(c =>
                            new MethodModelMapper().MapDown(c,
                                constructorsProperty?.PropertyType.GetGenericArguments()[0])).ToList()));
            }

            if (model.Properties != null)
            {
                PropertyInfo propertiesProperty = typeModelType.GetProperty("Properties",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                propertiesProperty?.SetValue(typModel,
                    HelperClass.ConvertList(propertiesProperty.PropertyType.GetGenericArguments()[0],
                        model.Properties?.Select(c =>
                            new PropertyModelMapper().MapDown(c,
                                propertiesProperty?.PropertyType.GetGenericArguments()[0])).ToList()));
            }
        }

        private void FillType(BaseTypeModel model, TypeModel typeModel)
        {
            typeModel.Name = model.Name;
            typeModel.IsExternal = model.IsExternal;
            typeModel.IsGeneric = model.IsGeneric;
            typeModel.Type = model.Type;
            typeModel.AssemblyName = model.AssemblyName;
            typeModel.Modifiers = model.Modifiers ?? new TypeModifiers();

            Type type = model.GetType();
            PropertyInfo baseTypeProperty = type.GetProperty("BaseType", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseTypeModel baseType = (BaseTypeModel)baseTypeProperty?.GetValue(model);
            typeModel.BaseType = EmitType(baseType);

            PropertyInfo declaringTypeProperty = type.GetProperty("DeclaringType", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            BaseTypeModel declaringType = (BaseTypeModel)declaringTypeProperty?.GetValue(model);
            typeModel.DeclaringType = EmitType(declaringType);

            PropertyInfo nestedTypesProperty = type.GetProperty("NestedTypes",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (nestedTypesProperty?.GetValue(model) != null)
            {
                List<BaseTypeModel> nestedTypes = (List<BaseTypeModel>)HelperClass.ConvertList(typeof(BaseTypeModel),
                    (IList)nestedTypesProperty?.GetValue(model));
                typeModel.NestedTypes = nestedTypes?.Select(n => EmitType(n)).ToList();
            }

            PropertyInfo genericArgumentsProperty = type.GetProperty("GenericArguments",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (genericArgumentsProperty?.GetValue(model) != null)
            {
                List<BaseTypeModel> genericArguments =
                    (List<BaseTypeModel>)HelperClass.ConvertList(typeof(BaseTypeModel),
                        (IList)genericArgumentsProperty?.GetValue(model));
                typeModel.GenericArguments = genericArguments?.Select(g => EmitType(g)).ToList();
            }

            PropertyInfo implementedInterfacesProperty = type.GetProperty("ImplementedInterfaces", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (implementedInterfacesProperty?.GetValue(model) != null)
            {
                List<BaseTypeModel> implementedInterfaces =
                    (List<BaseTypeModel>)HelperClass.ConvertList(typeof(BaseTypeModel),
                        (IList)implementedInterfacesProperty?.GetValue(model));
                typeModel.ImplementedInterfaces =
                    implementedInterfaces?.Select(i => EmitType(i)).ToList();
            }

            PropertyInfo fieldsProperty = type.GetProperty("Fields", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (fieldsProperty?.GetValue(model) != null)
            {
                List<BaseParameterModel> fields =
                    (List<BaseParameterModel>)HelperClass.ConvertList(typeof(BaseParameterModel),
                        (IList)fieldsProperty?.GetValue(model));
                typeModel.Fields = fields?.Select(g => new ParameterModelMapper().MapUp(g))
                    .ToList();
            }

            PropertyInfo methodsProperty = type.GetProperty("Methods", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (methodsProperty?.GetValue(model) != null)
            {
                List<BaseMethodModel> methods = (List<BaseMethodModel>)HelperClass.ConvertList(typeof(BaseMethodModel),
                    (IList)methodsProperty?.GetValue(model));
                typeModel.Methods = methods?.Select(c => new MethodModelMapper().MapUp(c)).ToList();
            }

            PropertyInfo constructorsProperty = type.GetProperty("Constructors",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (constructorsProperty?.GetValue(model) != null)
            {
                List<BaseMethodModel> constructors =
                    (List<BaseMethodModel>)HelperClass.ConvertList(typeof(BaseMethodModel),
                        (IList)constructorsProperty?.GetValue(model));
                typeModel.Constructors = constructors?.Select(c => new MethodModelMapper().MapUp(c))
                    .ToList();
            }

            PropertyInfo propertiesProperty = type.GetProperty("Properties", 
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            if (propertiesProperty?.GetValue(model) != null)
            {
                List<BasePropertyModel> properties =
                    (List<BasePropertyModel>)HelperClass.ConvertList(typeof(BasePropertyModel),
                        (IList)propertiesProperty?.GetValue(model));
                typeModel.Properties = properties?.Select(g => new PropertyModelMapper().MapUp(g))
                    .ToList();
            }
        }


        #region IModelMapper

        public TypeModel MapUp(BaseTypeModel model)
        {
            TypeModel typeModel = new TypeModel();
            if (model == null)
                return null;

            if (!Types.ContainsKey(model.Name))
            {
                Types.Add(model.Name, typeModel);
                FillType(model, typeModel);
            }
            return Types[model.Name];

        }

        public BaseTypeModel MapDown(TypeModel model, Type typeModelType)
        {

            object typeModel = Activator.CreateInstance(typeModelType);
            if (model == null)
                return null;
            if (!BaseTypes.ContainsKey(model.Name))
            {
                BaseTypes.Add(model.Name, (BaseTypeModel)typeModel);
                FillBaseType(model, (BaseTypeModel)typeModel);
            }
            return BaseTypes[model.Name];
        }

        #endregion
    }
}
