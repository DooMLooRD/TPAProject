using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Model.Assembly;
using DataLayer.DataModel;
using FileData.XMLModel;

namespace XMLModelMapper
{
    public class XMLTypeMapper
    {
        public static Dictionary<string, XMLTypeModel> XMLTypes = new Dictionary<string, XMLTypeModel>();
        public static Dictionary<string, TypeModel> Types = new Dictionary<string, TypeModel>();

        public static XMLTypeModel EmitXMLType(TypeModel model)
        {
            return new XMLTypeMapper().MapDown(model);
        }

        public static TypeModel EmitType(XMLTypeModel model)
        {
            return new XMLTypeMapper().MapUp(model);
        }

        private void FillXMLType(TypeModel model, XMLTypeModel typModel)
        {
            typModel.Name = model.Name;
            typModel.IsExternal = model.IsExternal;
            typModel.IsGeneric = model.IsGeneric;
            typModel.Type = model.Type;
            typModel.AssemblyName = model.AssemblyName;
            typModel.Modifiers = model.Modifiers ?? new TypeModifiers();

            typModel.BaseType = EmitXMLType(model.BaseType);
            typModel.DeclaringType = EmitXMLType(model.DeclaringType);

            typModel.NestedTypes = model.NestedTypes?.Select(c => EmitXMLType(c)).ToList();
            typModel.GenericArguments = model.GenericArguments?.Select(c => EmitXMLType(c)).ToList();
            typModel.ImplementedInterfaces = model.ImplementedInterfaces?.Select(c => EmitXMLType(c)).ToList();

            typModel.Fields = model.Fields?.Select(c => new XMLParameterMapper().MapDown(c)).ToList();
            typModel.Methods = model.Methods?.Select(m => new XMLMethodMapper().MapDown(m)).ToList();
            typModel.Constructors = model.Constructors?.Select(c => new XMLMethodMapper().MapDown(c)).ToList();
            typModel.Properties = model.Properties?.Select(c => new XMLPropertyMapper().MapDown(c)).ToList();
        }

        private void FillType(XMLTypeModel model, TypeModel typeModel)
        {
            typeModel.Name = model.Name;
            typeModel.IsExternal = model.IsExternal;
            typeModel.IsGeneric = model.IsGeneric;
            typeModel.Type = model.Type;
            typeModel.AssemblyName = model.AssemblyName;
            typeModel.Modifiers = model.Modifiers ?? new TypeModifiers();

            typeModel.BaseType = EmitType((XMLTypeModel)model.BaseType);
            typeModel.DeclaringType = EmitType((XMLTypeModel)model.DeclaringType);

            typeModel.NestedTypes = model.NestedTypes?.Select(n => EmitType((XMLTypeModel)n)).ToList();
            typeModel.GenericArguments = model.GenericArguments?.Select(g => EmitType((XMLTypeModel)g)).ToList();
            typeModel.ImplementedInterfaces = model.ImplementedInterfaces?.Select(i => EmitType((XMLTypeModel)i)).ToList();

            typeModel.Fields = model.Fields?.Select(g => new XMLParameterMapper().MapUp((XMLParameterModel)g)).ToList();
            typeModel.Methods = model.Methods?.Select(c => new XMLMethodMapper().MapUp((XMLMethodModel)c)).ToList();
            typeModel.Constructors = model.Constructors?.Select(c => new XMLMethodMapper().MapUp((XMLMethodModel)c)).ToList();
            typeModel.Properties = model.Properties?.Select(g => new XMLPropertyMapper().MapUp((XMLPropertyModel)g)).ToList();
        }


        #region IModelMapper

        public TypeModel MapUp(XMLTypeModel model)
        {
            TypeModel typeModel = new TypeModel();
            if (model == null)
                return null;

            if (!Types.ContainsKey(model.Name))
            {
                Types.Add(model.Name, typeModel);
                FillType(model, typeModel);
            }
            return Types[model.Name];

        }

        public XMLTypeModel MapDown(TypeModel model)
        {
            XMLTypeModel typeModel = new XMLTypeModel();
            if (model == null)
                return null;
            if (!XMLTypes.ContainsKey(model.Name))
            {
                XMLTypes.Add(model.Name, typeModel);
                FillXMLType(model, typeModel);
            }
            return XMLTypes[model.Name];
        }

        #endregion
    }
}
