using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Reflection;


namespace BusinessLogic.Model
{
    [DataContract(IsReference = true)]
    public class TypeModel
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string AssemblyName { get; set; }
        [DataMember]
        public bool IsExternal { get; set; } = true;
        [DataMember]
        public bool IsGeneric { get; set; } 
        [DataMember]
        public TypeModel BaseType { get; set; }
        [DataMember]
        public List<TypeModel> GenericArguments { get; set; }
        [DataMember]
        public Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> Modifiers { get; set; }
        [DataMember]
        public TypeEnum Type { get; set; }
        [DataMember]
        public List<TypeModel> ImplementedInterfaces { get; set; }
        [DataMember]
        public List<TypeModel> NestedTypes { get; set; }
        [DataMember]
        public List<PropertyModel> Properties { get; set; }
        [DataMember]
        public TypeModel DeclaringType { get; set; }
        [DataMember]
        public List<MethodModel> Methods { get; set; }
        [DataMember]
        public List<MethodModel> Constructors { get; set; }
        [DataMember]
        public List<ParameterModel> Fields { get; set; }

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

        private static Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> EmitModifiers(Type type)
        {
            AccessLevel _access = type.IsPublic || type.IsNestedPublic ? AccessLevel.Public :
                type.IsNestedFamily ? AccessLevel.Protected :
                type.IsNestedFamANDAssem ? AccessLevel.Internal :
                AccessLevel.Private;
            StaticEnum _static = type.IsSealed && type.IsAbstract ? StaticEnum.Static : StaticEnum.NotStatic;
            SealedEnum _sealed = SealedEnum.NotSealed;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (_static == StaticEnum.NotStatic)
            {
                _sealed = type.IsSealed ? SealedEnum.Sealed : SealedEnum.NotSealed;
                _abstract = type.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;
            }



            return new Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum>(_access, _sealed, _abstract, _static);
        }

        private static TypeModel EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            return EmitReference(baseType);
        }
        private bool _isAnalyzed = false;

        #endregion


    }
}
