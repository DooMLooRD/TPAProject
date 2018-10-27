using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns true if Type is public or nested public or nested family or nested family and assembly
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool GetVisible(this Type type)
        {
            return type.IsPublic || type.IsNestedPublic || type.IsNestedFamily || type.IsNestedFamANDAssem;
        }

        /// <summary>
        /// Returns true if method if not null and method is public or family or family and assembly
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        internal static bool GetVisible(this MethodBase method)
        {
            return method != null && (method.IsPublic || method.IsFamily || method.IsFamilyAndAssembly);
        }

        /// <summary>
        /// Gets namespace for given type ( if namespace is null returns empty string )
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static string GetNamespace(this Type type)
        {
            string ns = type.Namespace;
            return ns ?? string.Empty;
        }
    }
}
