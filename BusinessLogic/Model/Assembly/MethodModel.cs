using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using DataLayer.DataModel;
using DataLayer.DataModel.Enums;


namespace BusinessLogic.Model.Assembly
{
    
    public class MethodModel
    {
        public string Name { get; set; }
        /// <summary>
        /// List of Generic arguments
        /// </summary>
        public List<TypeModel> GenericArguments { get; set; }

        /// <summary>
        /// Tuple of modifiers for method ( Access level, Abstract, Static, Virtual)
        /// </summary>
        public MethodModifiers Modifiers { get; set; }

        /// <summary>
        /// The type that method returns
        /// </summary>
        public TypeModel ReturnType { get; set; }

        /// <summary>
        /// True if method is extension method 
        /// </summary>
        public bool Extension { get; set; }

        /// <summary>
        /// Parameters of the method
        /// </summary>
        public List<ParameterModel> Parameters { get; set; }

        public MethodModel()
        {
            
        }
        /// <summary>
        /// Constructor with MethodBase parameter
        /// </summary>
        /// <param name="method"></param>
        public MethodModel(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? null : EmitGenericArguments(method);
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method);
            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        private List<TypeModel> EmitGenericArguments(MethodBase method)
        {
            return method.GetGenericArguments().Select(TypeModel.EmitReference).ToList();
        }

        /// <summary>
        /// Emits MethodModels collection from MetodBase collection
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<MethodModel> EmitMethods(Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                   BindingFlags.Static | BindingFlags.Instance).Select(t => new MethodModel(t)).ToList();
        }

        /// <summary>
        /// Emits ParametersModels collection from ParameterInfo collection
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static List<ParameterModel> EmitParameters(MethodBase method)
        {
            return method.GetParameters().Select(t => new ParameterModel(t.Name,TypeModel.EmitReference(t.ParameterType))).ToList( );
        }

        /// <summary>
        /// Emits TypeModel to return from MethodBase 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static TypeModel EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return TypeModel.EmitReference(methodInfo.ReturnType);
        }

        /// <summary>
        /// Emits if Method is extension method or not from MethodBase
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        /// <summary>
        /// Emits Modifiers from MethodBase
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private static MethodModifiers EmitModifiers(MethodBase method)
        {
            AccessLevel access = method.IsPublic ? AccessLevel.Public :
                method.IsFamily ? AccessLevel.Protected :
                method.IsAssembly ? AccessLevel.Internal : AccessLevel.Private;

            AbstractEnum _abstract = method.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;

            StaticEnum _static = method.IsStatic ? StaticEnum.Static : StaticEnum.NotStatic;

            VirtualEnum _virtual = method.IsVirtual ? VirtualEnum.Virtual : VirtualEnum.NotVirtual;

            return new MethodModifiers()
            {
                AbstractEnum = _abstract,
                StaticEnum = _static,
                VirtualEnum = _virtual,
                AccessLevel = access
            };
        }

        public static List<MethodModel> EmitConstructors(Type type)
        {
            return type.GetConstructors().Select(t => new MethodModel(t)).ToList();
        }

        public override string ToString()
        {
            string type = String.Empty;
            type += Modifiers.AccessLevel.ToString().ToLower() + " ";
            type += Modifiers.AbstractEnum == AbstractEnum.Abstract ? AbstractEnum.Abstract.ToString().ToLower() + " " : String.Empty;
            type += Modifiers.StaticEnum == StaticEnum.Static ? StaticEnum.Static.ToString().ToLower() + " " : String.Empty;
            type += Modifiers.VirtualEnum == VirtualEnum.Virtual ? VirtualEnum.Virtual.ToString().ToLower() + " " : String.Empty;
            type += ReturnType != null ? ReturnType.Name + " " : String.Empty;
            type += Name;
            type += Extension ? " :Extension method" : String.Empty;
            return type;
        }
    }
}
