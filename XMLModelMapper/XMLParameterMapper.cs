using BusinessLogic.Model.Assembly;
using FileData.XMLModel;

namespace XMLModelMapper
{
    public class XMLParameterMapper 
    {
        public ParameterModel MapUp(XMLParameterModel model)
        {
            ParameterModel parameterModel = new ParameterModel();
            parameterModel.Name = model.Name;
            if (model.Type != null)
                parameterModel.Type = XMLTypeMapper.EmitType((XMLTypeModel)model.Type);
            return parameterModel;
        }

        public XMLParameterModel MapDown(ParameterModel model)
        {
            XMLParameterModel parameterModel = new XMLParameterModel();
            parameterModel.Name = model.Name;
            if (model.Type != null)
                parameterModel.Type = XMLTypeMapper.EmitXMLType(model.Type);
            return parameterModel;
        }
    }
}
