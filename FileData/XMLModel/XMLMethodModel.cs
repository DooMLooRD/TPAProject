using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using BusinessLogic.Model.Assembly;
using MEF;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLMethodModel : IModelMapper<MethodModel, XMLMethodModel> 
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public List<XMLTypeModel> GenericArguments { get; set; }
        [DataMember] public MethodModifiers Modifiers { get; set; }
        [DataMember] public XMLTypeModel ReturnType { get; set; }
        [DataMember] public bool Extension { get; set; }
        [DataMember] public List<XMLParameterModel> Parameters { get; set; }
        public MethodModel MapUp(XMLMethodModel model)
        {
            MethodModel methodModel=new MethodModel();
            methodModel.Name = model.Name;
            methodModel.Extension = model.Extension;
            if (model.GenericArguments != null)
                methodModel.GenericArguments = model.GenericArguments.Select(XMLTypeModel.GetType).ToList();
            methodModel.Modifiers = model.Modifiers;
            if (model.Parameters != null)
                methodModel.Parameters = model.Parameters.Select(p => p.MapUp(p)).ToList();
            if (model.ReturnType != null)
                methodModel.ReturnType = XMLTypeModel.GetType(model.ReturnType);
            return methodModel;
        }

        public XMLMethodModel MapDown(MethodModel model)
        {
            Name = model.Name;
            Extension = model.Extension;
            if (model.GenericArguments != null)
                GenericArguments = model.GenericArguments.Select(XMLTypeModel.GetXMLType).ToList();
            Modifiers = model.Modifiers;
            if (model.Parameters != null)
                Parameters = model.Parameters.Select(p => new XMLParameterModel().MapDown(p)).ToList();
            if (model.ReturnType != null)
                ReturnType = XMLTypeModel.GetXMLType(model.ReturnType);
            return this;
        }
    }
}
