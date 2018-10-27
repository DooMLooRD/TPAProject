using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    [DataContract(IsReference = true)]
    public class NamespaceModel
    {
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// The List of types in the namespace
        /// </summary>
        [DataMember]
        public List<TypeModel> Types { get; set; }

        /// <summary>
        /// Constructor with name of the namespace and types as params 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="types"></param>
        public NamespaceModel(string name, List<Type> types)
        {
            Name = name;
            Types = types.OrderBy(t => t.Name).Select(t => new TypeModel(t)).ToList();
        }

    }
}
