using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class TypeModel : BaseModel
    {
        //TODO: Write comments for everthing in TypeModel class

        public static Dictionary<string, TypeModel> TypeDictionary = new Dictionary<string, TypeModel>();
        public string NamespaceName { get; set; }
        public TypeModel BaseType { get; set; }
        public List<TypeModel> GenericArguments { get; set; }
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; set; }
        public TypeKind Type { get; set; }
        public List<TypeModel> ImplementedInterfaces { get; set; }
        public List<TypeModel> NestedTypes { get; set; }
        public List<PropertyModel> Properties { get; set; }
        public TypeModel DeclaringType { get; set; }
        public List<MethodModel> Methods { get; set; }
        public List<MethodModel> Constructors { get; set; }
        public List<ParameterModel> Fields { get; set; }

        static Tuple<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            AccessLevel _access = AccessLevel.Private;
            if (type.IsPublic)
                _access = AccessLevel.Public;
            else if (type.IsNestedPublic)
                _access = AccessLevel.Public;
            else if (type.IsNestedFamily)
                _access = AccessLevel.Protected;
            else if (type.IsNestedFamANDAssem)
                _access = AccessLevel.Internal;
            SealedEnum _sealed = SealedEnum.NotSealed;
            if (type.IsSealed) _sealed = SealedEnum.Sealed;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (type.IsAbstract)
                _abstract = AbstractEnum.Abstract;
            return new Tuple<AccessLevel, SealedEnum, AbstractEnum>(_access, _sealed, _abstract);
        }
        public TypeModel(Type type) : base(type.Name)
        {
            if (!TypeDictionary.ContainsKey(Name))
            {
                TypeDictionary.Add(Name, this);
            }

            DeclaringType = EmitDeclaringType(type.DeclaringType);
            Constructors = MethodModel.EmitMethods(type.GetConstructors()).ToList();
            Methods = MethodModel.EmitMethods(type.GetMethods()).ToList();
            NestedTypes = EmitNestedTypes(type.GetNestedTypes()).ToList();
            ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            GenericArguments = !type.IsGenericTypeDefinition ? null : TypeModel.EmitGenericArguments(type.GetGenericArguments()).ToList();
            Modifiers = EmitModifiers(type);
            BaseType = EmitExtends(type.BaseType);
            Properties = PropertyModel.EmitProperties(type.GetProperties()).ToList();
            Type = GetTypeKind(type);
            Fields = EmitFields(type.GetFields()).ToList();
        }

        private TypeModel(string typeName, string namespaceName) : base(typeName)
        {
            this.NamespaceName = namespaceName;
        }

        private TypeModel(string typeName, string namespaceName, IEnumerable<TypeModel> genericArguments) : this(typeName, namespaceName)
        {
            this.GenericArguments = genericArguments.ToList();
        }

        public enum TypeKind
        {
            Enum, Struct, Interface, Class
        }

        public static TypeModel EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new TypeModel(type.Name, type.GetNamespace());
            else
                return new TypeModel(type.Name, type.GetNamespace(), EmitGenericArguments(type.GetGenericArguments()));
        }
        public static IEnumerable<TypeModel> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            foreach (Type typ in arguments)
            {
                StoreType(typ);
            }
            return from Type _argument in arguments select EmitReference(_argument);
        }

        public static void StoreType(Type type)
        {
            if (!TypeDictionary.ContainsKey(type.Name))
            {
                new TypeModel(type);
            }
        }

        private static IEnumerable<ParameterModel> EmitFields(IEnumerable<FieldInfo> fieldInfo)
        {
            List<ParameterModel> parameters = new List<ParameterModel>();
            foreach (FieldInfo field in fieldInfo)
            {
                StoreType(field.FieldType);
                parameters.Add(new ParameterModel(field.Name, TypeModel.EmitReference(field.FieldType)));
            }
            return parameters;
        }

        private TypeModel EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            StoreType(declaringType);
            return EmitReference(declaringType);
        }
        private IEnumerable<TypeModel> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            foreach (Type typ in nestedTypes)
            {
                StoreType(typ);
            }

            return from _type in nestedTypes
                   where _type.GetVisible()
                   select new TypeModel(_type);
        }
        private IEnumerable<TypeModel> EmitImplements(IEnumerable<Type> interfaces)
        {
            foreach (Type @interface in interfaces)
            {
                StoreType(@interface);
            }

            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }
        private static TypeKind GetTypeKind(Type type)
        {
            return type.IsEnum ? TypeKind.Enum :
                   type.IsValueType ? TypeKind.Struct :
                   type.IsInterface ? TypeKind.Interface :
                   TypeKind.Class;
        }

        private static TypeModel EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            StoreType(baseType);
            return EmitReference(baseType);
        }
    }
}
