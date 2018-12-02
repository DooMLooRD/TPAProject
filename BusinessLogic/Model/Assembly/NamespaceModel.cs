using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BusinessLogic.Model.Assembly
{
    public class NamespaceModel 
    {
        public string Name { get; set; }
        /// <summary>
        /// The List of types in the namespace
        /// </summary>
        public List<TypeModel> Types { get; set; }

        public NamespaceModel()
        {
            
        }
        /// <summary>
        /// Constructor with name of the namespace and types as params 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="types"></param>
        public NamespaceModel(string name, List<Type> types)
        {
            Name = name;
            Types = types.OrderBy(t => t.Name).Select(TypeModel.EmitType).ToList();
        }
    }
}
