using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    /// <summary>
    /// Base model for metadata
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Name of metada
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Constructor with Name as parameter
        /// </summary>
        /// <param name="name"></param>
        public BaseModel(string name)
        {
            Name = name;
        }
    }
}
