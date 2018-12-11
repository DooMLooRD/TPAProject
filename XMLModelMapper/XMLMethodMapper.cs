using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogic.Model.Assembly;
using FileData.XMLModel;

namespace XMLModelMapper
{
    public class XMLMethodMapper
    {

        public MethodModel MapUp(XMLMethodModel model)
        {
            MethodModel methodModel = new MethodModel();
            methodModel.Name = model.Name;
            methodModel.Extension = model.Extension;
            if (model.GenericArguments != null)
                methodModel.GenericArguments = model.GenericArguments.Select(g=> XMLTypeMapper.EmitType((XMLTypeModel)g)).ToList();
            methodModel.Modifiers = model.Modifiers;
            if (model.Parameters != null)
                methodModel.Parameters = model.Parameters.Select(p => new XMLParameterMapper().MapUp((XMLParameterModel)p)).ToList();
            if (model.ReturnType != null)
                methodModel.ReturnType = XMLTypeMapper.EmitType((XMLTypeModel)model.ReturnType);
            return methodModel;
        }

        public XMLMethodModel MapDown(MethodModel model)
        {
            XMLMethodModel methodModel = new XMLMethodModel();
            methodModel.Name = model.Name;
            methodModel.Extension = model.Extension;
            if (model.GenericArguments != null)
                methodModel.GenericArguments = model.GenericArguments.Select(t=>XMLTypeMapper.EmitXMLType(t)).ToList();
            methodModel.Modifiers = model.Modifiers;
            if (model.Parameters != null)
                methodModel.Parameters = model.Parameters.Select(p => new XMLParameterMapper().MapDown(p)).ToList();
            if (model.ReturnType != null)
                methodModel.ReturnType = XMLTypeMapper.EmitXMLType(model.ReturnType);
            return methodModel;
        }
    }
}
