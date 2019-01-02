using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataLayer.DataModel;

namespace FileData.XMLModel
{

    public class XMLMethodModel : BaseMethodModel
    {
        public override string Name { get; set; }

        public new List<XMLTypeModel> GenericArguments { get; set; }

        public new XMLTypeModel ReturnType { get; set; }

        public override bool Extension { get; set; }

        public new List<XMLParameterModel> Parameters { get; set; }

        public override MethodModifiers Modifiers { get; set; }
    }
}
