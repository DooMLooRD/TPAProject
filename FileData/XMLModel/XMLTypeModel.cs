using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using BusinessLogic.Model.Assembly;
using BusinessLogic.Model.Enums;
using MEF;

namespace FileData.XMLModel
{
    [DataContract(IsReference = true)]
    public class XMLTypeModel : IModelMapper<TypeModel, XMLTypeModel>
    {
        public static Dictionary<string, XMLTypeModel> XMLTypes = new Dictionary<string, XMLTypeModel>();
        public static Dictionary<string, TypeModel> Types = new Dictionary<string, TypeModel>();

        public static XMLTypeModel GetXMLType(TypeModel model)
        {

            if (!XMLTypes.ContainsKey(model.Name))
            {
                XMLTypes.Add(model.Name, new XMLTypeModel().MapDown(model));
            }

            return XMLTypes[model.Name];


        }
        public static TypeModel GetType(XMLTypeModel model)
        {

            if (!Types.ContainsKey(model.Name))
            {
                Types.Add(model.Name, model.MapUp(model));
            }

            return Types[model.Name];
        }

        private void FillXMLType(TypeModel model)
        {
            if (model.AssemblyName != null)
                AssemblyName = model.AssemblyName;
            if (model.BaseType != null)
                BaseType = GetXMLType(model.BaseType);
            if (model.Constructors != null)
                Constructors = model.Constructors.Select(c => new XMLMethodModel().MapDown(c)).ToList();
            if (model.DeclaringType != null)
                DeclaringType = GetXMLType(model.DeclaringType);
            if (model.Fields != null)
                Fields = model.Fields?.Select(g => new XMLParameterModel().MapDown(g)).ToList();
            if (model.GenericArguments != null)
                GenericArguments = model.GenericArguments.Select(GetXMLType).ToList();
            if (model.ImplementedInterfaces != null)
                ImplementedInterfaces = model.ImplementedInterfaces.Select(GetXMLType).ToList();
            if (model.Methods != null)
                Methods = model.Methods.Select(c => new XMLMethodModel().MapDown(c)).ToList();
            if (model.Modifiers != null)
                Modifiers = model.Modifiers;
            if (model.NestedTypes != null)
                NestedTypes = model.NestedTypes.Select(GetXMLType).ToList();
            if (model.Properties != null)
                Properties = model.Properties.Select(g => new XMLPropertyModel().MapDown(g)).ToList();
        }
        private void FillType(XMLTypeModel xmlType, TypeModel typeModel)
        {
            if (xmlType.AssemblyName != null)
                typeModel.AssemblyName = xmlType.AssemblyName;
            if (xmlType.BaseType != null)
                typeModel.BaseType = GetType(xmlType.BaseType);
            if (xmlType.Constructors != null)
                typeModel.Constructors = xmlType.Constructors.Select(c => c.MapUp(c)).ToList();
            if (xmlType.DeclaringType != null)
                typeModel.DeclaringType = GetType(xmlType.DeclaringType);
            if (xmlType.Fields != null)
                typeModel.Fields = xmlType.Fields?.Select(g => g.MapUp(g)).ToList();
            if (xmlType.GenericArguments != null)
                typeModel.GenericArguments = xmlType.GenericArguments.Select(GetType).ToList();
            if (xmlType.ImplementedInterfaces != null)
                typeModel.ImplementedInterfaces = xmlType.ImplementedInterfaces.Select(GetType).ToList();
            if (xmlType.Methods != null)
                typeModel.Methods = xmlType.Methods.Select(c => c.MapUp(c)).ToList();
            if (xmlType.Modifiers != null)
                typeModel.Modifiers = xmlType.Modifiers;
            if (xmlType.NestedTypes != null)
                typeModel.NestedTypes = xmlType.NestedTypes.Select(GetType).ToList();
            if (xmlType.Properties != null)
                typeModel.Properties = xmlType.Properties.Select(g => g.MapUp(g)).ToList();
        }

        [DataMember] public string Name { get; set; }
        [DataMember] public string AssemblyName { get; set; }
        [DataMember] public bool IsExternal { get; set; }
        [DataMember] public bool IsGeneric { get; set; }
        [DataMember] public XMLTypeModel BaseType { get; set; }
        [DataMember] public List<XMLTypeModel> GenericArguments { get; set; }
        [DataMember] public TypeModifiers Modifiers { get; set; }
        [DataMember] public TypeEnum Type { get; set; }
        [DataMember] public List<XMLTypeModel> ImplementedInterfaces { get; set; }
        [DataMember] public List<XMLTypeModel> NestedTypes { get; set; }
        [DataMember] public List<XMLPropertyModel> Properties { get; set; }
        [DataMember] public XMLTypeModel DeclaringType { get; set; }
        [DataMember] public List<XMLMethodModel> Methods { get; set; }
        [DataMember] public List<XMLMethodModel> Constructors { get; set; }
        [DataMember] public List<XMLParameterModel> Fields { get; set; }

        public TypeModel MapUp(XMLTypeModel model)
        {
            TypeModel typeModel=new TypeModel();
            typeModel.Name = model.Name;
            typeModel.IsExternal = model.IsExternal;
            typeModel.IsGeneric = model.IsGeneric;
            typeModel.Type = model.Type;
            FillType(model, typeModel);
            return typeModel;
        }

        public XMLTypeModel MapDown(TypeModel model)
        {
            Name = model.Name;
            IsExternal = model.IsExternal;
            IsGeneric = model.IsGeneric;
            Type = model.Type;
            FillXMLType(model);
            return this;
        }
    }
}
