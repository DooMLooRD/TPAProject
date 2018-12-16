using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataLayer.DataModel;
using DataLayer.DataModel.Enums;


namespace BusinessLogic.Model.Assembly
{
    public class TypeModel
    {
        public string Name { get; set; }
        public string AssemblyName { get; set; }
        public bool IsExternal { get; set; } = true;
        public bool IsGeneric { get; set; }
        public TypeModel BaseType { get; set; }
        public List<TypeModel> GenericArguments { get; set; }
        public TypeModifiers Modifiers { get; set; }
        public TypeEnum Type { get; set; }
        public List<TypeModel> ImplementedInterfaces { get; set; }
        public List<TypeModel> NestedTypes { get; set; }
        public List<PropertyModel> Properties { get; set; }
        public TypeModel DeclaringType { get; set; }
        public List<MethodModel> Methods { get; set; }
        public List<MethodModel> Constructors { get; set; }
        public List<ParameterModel> Fields { get; set; }
        public List<TypeModel> Attributes { get; set; }

        public TypeModel()
        {

        }
        private TypeModel(Type type)
        {
            Name = type.Name;
            IsGeneric = type.IsGenericParameter;
            AssemblyName = type.AssemblyQualifiedName;
        }

        private void Analyze(Type type)
        {
            Type = GetTypeEnum(type);
            BaseType = EmitExtends(type.BaseType);
            Modifiers = EmitModifiers(type);

            DeclaringType = EmitDeclaringType(type.DeclaringType);
            Constructors = MethodModel.EmitConstructors(type);
            Methods = MethodModel.EmitMethods(type);
            NestedTypes = EmitNestedTypes(type);
            ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            GenericArguments = !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type);
            Properties = PropertyModel.EmitProperties(type);
            Fields = EmitFields(type);
            Attributes = EmitAttributes(type);
            IsExternal = false;
            _isAnalyzed = true;
        }


        public static TypeModel EmitType(Type type)
        {
            if (!DictionaryTypeSingleton.Instance.ContainsKey(type.Name))
            {
                DictionaryTypeSingleton.Instance.Add(type.Name, new TypeModel(type));
            }

            if (!DictionaryTypeSingleton.Instance.Get(type.Name)._isAnalyzed)
            {
                DictionaryTypeSingleton.Instance.Get(type.Name).Analyze(type);
            }

            return DictionaryTypeSingleton.Instance.Get(type.Name);
        }

        public static TypeModel EmitReference(Type type)
        {
            if (!DictionaryTypeSingleton.Instance.ContainsKey(type.Name))
            {
                DictionaryTypeSingleton.Instance.Add(type.Name, new TypeModel(type));

            }
            return DictionaryTypeSingleton.Instance.Get(type.Name);
        }

        public static List<TypeModel> EmitGenericArguments(Type type)
        {
            List<Type> arguments = type.GetGenericArguments().ToList();

            return arguments.Select(EmitReference).ToList();
        }


        #region Private Emits

        private static List<ParameterModel> EmitFields(Type type)
        {
            List<FieldInfo> fieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                           BindingFlags.Static | BindingFlags.Instance).ToList();

            List<ParameterModel> parameters = new List<ParameterModel>();
            foreach (FieldInfo field in fieldInfo)
            {
                parameters.Add(new ParameterModel(field.Name, EmitReference(field.FieldType)));
            }
            return parameters;
        }

        private TypeModel EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            return EmitReference(declaringType);
        }
        private List<TypeModel> EmitNestedTypes(Type type)
        {
            List<Type> nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).ToList();

            return nestedTypes.Select(EmitType).ToList();
        }

        private List<TypeModel> EmitAttributes(Type type)
        {
            List<TypeModel> attributes = type.GetCustomAttributes(false)
                .Select(a => new TypeModel(a.GetType())).ToList();

            return attributes;
        }
        private IEnumerable<TypeModel> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }
        private static TypeEnum GetTypeEnum(Type type)
        {
            return type.IsEnum ? TypeEnum.Enum :
                   type.IsValueType ? TypeEnum.Struct :
                   type.IsInterface ? TypeEnum.Interface :
                   TypeEnum.Class;
        }

        private static TypeModifiers EmitModifiers(Type type)
        {
            AccessLevel _access = type.IsPublic || type.IsNestedPublic ? AccessLevel.Public :
                type.IsNestedFamily ? AccessLevel.Protected :
                type.IsNestedPrivate ? AccessLevel.Private :
                AccessLevel.Internal;
            StaticEnum _static = type.IsSealed && type.IsAbstract ? StaticEnum.Static : StaticEnum.NotStatic;
            SealedEnum _sealed = SealedEnum.NotSealed;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (_static == StaticEnum.NotStatic)
            {
                _sealed = type.IsSealed ? SealedEnum.Sealed : SealedEnum.NotSealed;
                _abstract = type.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;
            }

            return new TypeModifiers()
            {
                AbstractEnum = _abstract,
                AccessLevel = _access,
                SealedEnum = _sealed,
                StaticEnum = _static
            };
        }

        private static TypeModel EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            return EmitReference(baseType);
        }
        private bool _isAnalyzed = false;

        #endregion

        public override string ToString()
        {
            string type = String.Empty;
            if (Modifiers != null)
            {
                type += Modifiers.AccessLevel.ToString().ToLower() + " ";
                type += Modifiers.SealedEnum == SealedEnum.Sealed ? SealedEnum.Sealed.ToString().ToLower() + " " : String.Empty;
                type += Modifiers.AbstractEnum == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : String.Empty;
                type += Modifiers.StaticEnum == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : String.Empty;

            }
            type += Type != TypeEnum.None ? Type.ToString().ToLower() + " " : String.Empty;
            type += Name;
            if (IsGeneric)
                type += " - generic type";
            else if (IsExternal)
                type += " - external assembly: " + AssemblyName;

            return type;
        }
    }
}
