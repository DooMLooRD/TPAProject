using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class NamespaceModel : BaseModel
    {
        /// <summary>
        /// The List of types in the namespace
        /// </summary>
        public List<TypeModel> Types { get; set; }

        /// <summary>
        /// Constructor with name of the namespace and types as params 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="types"></param>
        public NamespaceModel(string name, List<Type> types) : base(name)
        {
            Types = types.OrderBy(t => t.Name).Select(t => new TypeModel(t)).ToList();
        }

    }
}
