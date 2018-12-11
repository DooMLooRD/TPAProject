using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Model.Assembly;
using DataLayer.DataModel;
using DBData.Entities;

namespace DBModelMapper
{
    public class DBTypeMapper
    {


        public static Dictionary<string, DBTypeModel> XMLTypes = new Dictionary<string, DBTypeModel>();
        public static Dictionary<string, TypeModel> Types = new Dictionary<string, TypeModel>();

     

        public static DBTypeModel EmitDBType(TypeModel model)
        {
            return new DBTypeMapper().MapDown(model);
        }

        public static TypeModel EmitType(DBTypeModel model)
        {
            return new DBTypeMapper().MapUp(model);
        }

        private void FillDBType(TypeModel model, DBTypeModel typModel)
        {
            typModel.Name = model.Name;
            typModel.IsExternal = model.IsExternal;
            typModel.IsGeneric = model.IsGeneric;
            typModel.Type = model.Type;
            typModel.AssemblyName = model.AssemblyName;
            typModel.Modifiers = model.Modifiers ?? new TypeModifiers();

            typModel.BaseType = EmitDBType(model.BaseType);
            typModel.DeclaringType = EmitDBType(model.DeclaringType);

            typModel.NestedTypes = model.NestedTypes?.Select(c => EmitDBType(c)).ToList();
            typModel.GenericArguments = model.GenericArguments?.Select(c => EmitDBType(c)).ToList();
            typModel.ImplementedInterfaces = model.ImplementedInterfaces?.Select(c => EmitDBType(c)).ToList();

            typModel.Fields = model.Fields?.Select(c => new DBParameterMapper().MapDown(c)).ToList();
            typModel.Methods = model.Methods?.Select(m => new DBMethodMapper().MapDown(m)).ToList();
            typModel.Constructors = model.Constructors?.Select(c => new DBMethodMapper().MapDown(c)).ToList();
            typModel.Properties = model.Properties?.Select(c => new DBPropertyMapper().MapDown(c)).ToList();
        }

        private void FillType(DBTypeModel model, TypeModel typeModel)
        {
            typeModel.Name = model.Name;
            typeModel.IsExternal = model.IsExternal;
            typeModel.IsGeneric = model.IsGeneric;
            typeModel.Type = model.Type;
            typeModel.AssemblyName = model.AssemblyName;
            typeModel.Modifiers = model.Modifiers ?? new TypeModifiers();

            typeModel.BaseType = EmitType((DBTypeModel)model.BaseType);
            typeModel.DeclaringType = EmitType((DBTypeModel)model.DeclaringType);

            typeModel.NestedTypes = model.NestedTypes?.Select(n => EmitType((DBTypeModel)n)).ToList();
            typeModel.GenericArguments = model.GenericArguments?.Select(g => EmitType((DBTypeModel)g)).ToList();
            typeModel.ImplementedInterfaces = model.ImplementedInterfaces?.Select(i => EmitType((DBTypeModel)i)).ToList();

            typeModel.Fields = model.Fields?.Select(g => new DBParameterMapper().MapUp((DBParameterModel)g)).ToList();
            typeModel.Methods = model.Methods?.Select(c => new DBMethodMapper().MapUp((DBMethodModel)c)).ToList();
            typeModel.Constructors = model.Constructors?.Select(c => new DBMethodMapper().MapUp((DBMethodModel)c)).ToList();
            typeModel.Properties = model.Properties?.Select(g => new DBPropertyMapper().MapUp((DBPropertyModel)g)).ToList();
        }


        #region IModelMapper

        public TypeModel MapUp(DBTypeModel model)
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

        public DBTypeModel MapDown(TypeModel model)
        {
            DBTypeModel typeModel = new DBTypeModel();
            if (model == null)
                return null;
            if (!XMLTypes.ContainsKey(model.Name))
            {
                XMLTypes.Add(model.Name, typeModel);
                FillDBType(model, typeModel);
            }
            return XMLTypes[model.Name];
        }

        #endregion
    }
}
