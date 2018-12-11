using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BusinessLogic.Model.Assembly
{
    public class AssemblyModel 
    {
        /// <summary>
        /// The List of Namespaces in the Assembly
        /// </summary>
        public List<NamespaceModel> NamespaceModels { get; set; }
        public string Name { get; set; }

        public AssemblyModel()
        {
            
        }
        /// <summary>
        /// Constructor with assembly as a parameter
        /// </summary>
        /// <param name="assembly"></param>
        public AssemblyModel(System.Reflection.Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Type[] types = assembly.GetTypes();
            NamespaceModels = types.GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new NamespaceModel(t.Key, t.ToList())).ToList();
        }//Where(t => t.IsVisible)

    }
}
