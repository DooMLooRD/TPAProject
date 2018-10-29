using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    [DataContract(IsReference = true)]
    public class ParameterModel
    {
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// TypeModel of the parameter
        /// </summary>
        [DataMember]
        public TypeModel Type { get; set; }

        /// <summary>
        /// Constructor with name and TypeModel as params
        /// </summary>
        /// <param name="name"></param>
        /// <param name="typeModel"></param>
        public ParameterModel(string name, TypeModel typeModel)
        {
            Name = name;
            Type = typeModel;
        }
    }
}
