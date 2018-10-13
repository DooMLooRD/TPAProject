using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class ParameterModel : BaseModel
    {
        /// <summary>
        /// TypeModel of the parameter
        /// </summary>
        public TypeModel Type { get; set; }

        /// <summary>
        /// Constructor with name and TypeModel as params
        /// </summary>
        /// <param name="name"></param>
        /// <param name="typeModel"></param>
        public ParameterModel(string name, TypeModel typeModel) : base(name)
        {
            Type = typeModel;
        }
    }
}
