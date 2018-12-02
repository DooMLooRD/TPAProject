using System.Runtime.Serialization;
using BusinessLogic.Model.Assembly;
using MEF;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLParameterModel : IModelMapper<ParameterModel, XMLParameterModel>
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public XMLTypeModel Type { get; set; }
        public ParameterModel MapUp(XMLParameterModel model)
        {
            ParameterModel parameterModel=new ParameterModel();
            parameterModel.Name = model.Name;
            if (model.Type != null)
                parameterModel.Type = XMLTypeModel.GetType(model.Type);
            return parameterModel;
        }

        public XMLParameterModel MapDown(ParameterModel model)
        {
            Name = model.Name;
            if (model.Type != null)
                Type = XMLTypeModel.GetXMLType(model.Type);
            return this;
        }
    }
}
