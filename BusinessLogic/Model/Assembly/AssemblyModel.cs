using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    [DataContract]
    public class AssemblyModel
    {
        /// <summary>
        /// The List of Namespaces in the Assembly
        /// </summary>
        [DataMember]
        public List<NamespaceModel> NamespaceModels { get; set; }

        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Constructor with assembly as a parameter
        /// </summary>
        /// <param name="assembly"></param>
        public AssemblyModel(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Type[] types = assembly.GetTypes();
            NamespaceModels = types.Where(t => t.IsVisible).GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new NamespaceModel(t.Key, t.ToList())).ToList();
        }

    }
}
