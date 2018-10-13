using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class MethodModel : BaseModel
    {
        /// <summary>
        /// List of Generic arguments
        /// </summary>
        public List<TypeModel> GenericArguments { get; set; }

        /// <summary>
        /// Tuple of modifiers for method ( Access level, Abstract, Static, Virtual)
        /// </summary>
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

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

        /// <summary>
        /// Constructor with MethodBase parameter
        /// </summary>
        /// <param name="method"></param>
        public MethodModel(MethodBase method) : base(method.Name)
        {
            GenericArguments = !method.IsGenericMethodDefinition ? null : TypeModel.EmitGenericArguments(method.GetGenericArguments()).ToList();
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method.GetParameters()).ToList();
            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        /// <summary>
        /// Emits MethodModels collection from MetodBase collection
        /// </summary>
        /// <param name="methods"></param>
        /// <returns></returns>
        public static IEnumerable<MethodModel> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase _currentMethod in methods
                   where _currentMethod.GetVisible()
                   select new MethodModel(_currentMethod);
        }

        /// <summary>
        /// Emits ParametersModels collection from ParameterInfo collection
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        private static IEnumerable<ParameterModel> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return from parm in parms
                   select new ParameterModel(parm.Name, TypeModel.EmitReference(parm.ParameterType));
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
            TypeModel.StoreType(methodInfo.ReturnType);
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
        private static Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevel _access = AccessLevel.Private;
            if (method.IsPublic)
                _access = AccessLevel.Public;
            else if (method.IsFamily)
                _access = AccessLevel.Protected;
            else if (method.IsFamilyAndAssembly)
                _access = AccessLevel.Internal;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (method.IsAbstract)
                _abstract = AbstractEnum.Abstract;
            StaticEnum _static = StaticEnum.NotStatic;
            if (method.IsStatic)
                _static = StaticEnum.Static;
            VirtualEnum _virtual = VirtualEnum.NotVirtual;
            if (method.IsVirtual)
                _virtual = VirtualEnum.Virtual;
            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(_access, _abstract, _static, _virtual);
        }
    }
}
