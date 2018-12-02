using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessLogic.Model.Assembly;
using BusinessLogic.Model.Enums;
using MEF;

namespace DBData.Entities
{
    [Table("TypeModel")]
    public class DBTypeModel : IModelMapper<TypeModel, DBTypeModel>
    {

        #region Type Dictionary

        public static Dictionary<string, DBTypeModel> DBTypes = new Dictionary<string, DBTypeModel>();
        public static Dictionary<string, TypeModel> Types = new Dictionary<string, TypeModel>();

        #endregion

        #region Constructor

        public DBTypeModel()
        {
            MethodGenericArguments = new HashSet<DBMethodModel>();
            Constructors = new HashSet<DBMethodModel>();
            Fields = new HashSet<DBParameterModel>();
            TypeGenericArguments = new HashSet<DBTypeModel>();
            GenericArguments = new HashSet<DBTypeModel>();
            TypeImplementedInterfaces = new HashSet<DBTypeModel>();
            ImplementedInterfaces = new HashSet<DBTypeModel>();
            Methods = new HashSet<DBMethodModel>();
            TypeNestedTypes = new HashSet<DBTypeModel>();
            NestedTypes = new HashSet<DBTypeModel>();
            Properties = new HashSet<DBPropertyModel>();
        }

        #endregion

        #region Properties

        [Key, StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string AssemblyName { get; set; }

        public bool IsExternal { get; set; }

        public bool IsGeneric { get; set; }

        public DBTypeModel BaseType { get; set; }

        public TypeEnum Type { get; set; }
        public DBTypeModel DeclaringType { get; set; }

        public TypeModifiers Modifiers { get; set; }

        public int? NamespaceId { get; set; }

        public virtual ICollection<DBMethodModel> Constructors { get; set; }

        public virtual ICollection<DBParameterModel> Fields { get; set; }

        public virtual ICollection<DBTypeModel> GenericArguments { get; set; }

        public virtual ICollection<DBTypeModel> ImplementedInterfaces { get; set; }

        public virtual ICollection<DBMethodModel> Methods { get; set; }

        public virtual ICollection<DBTypeModel> NestedTypes { get; set; }

        public virtual ICollection<DBPropertyModel> Properties { get; set; }

        #endregion

        #region Inverse Properties

        [InverseProperty("BaseType")]
        public virtual ICollection<DBTypeModel> TypeBaseTypes { get; set; }

        [InverseProperty("DeclaringType")]
        public virtual ICollection<DBTypeModel> TypeDeclaringTypes { get; set; }

        public virtual ICollection<DBMethodModel> MethodGenericArguments { get; set; }

        public virtual ICollection<DBTypeModel> TypeGenericArguments { get; set; }

        public virtual ICollection<DBTypeModel> TypeImplementedInterfaces { get; set; }

        public virtual ICollection<DBTypeModel> TypeNestedTypes { get; set; }

        #endregion

        #region Emit and Fill methods

        public static DBTypeModel EmitDBType(TypeModel model)
        {
            return new DBTypeModel().MapDown(model);
        }

        public static TypeModel EmitType(DBTypeModel model)
        {
            return new DBTypeModel().MapUp(model);
        }

        private void FillDBType(TypeModel model)
        {
            Name = model.Name;
            IsExternal = model.IsExternal;
            IsGeneric = model.IsGeneric;
            Type = model.Type;
            AssemblyName = model.AssemblyName;
            Modifiers = model.Modifiers ?? new TypeModifiers();

            BaseType = EmitDBType(model.BaseType);
            DeclaringType = EmitDBType(model.DeclaringType);

            NestedTypes = model.NestedTypes?.Select(c => EmitDBType(c)).ToList();
            GenericArguments = model.GenericArguments?.Select(c => EmitDBType(c)).ToList();
            ImplementedInterfaces = model.ImplementedInterfaces?.Select(c => EmitDBType(c)).ToList();

            Fields = model.Fields?.Select(c => new DBParameterModel().MapDown(c)).ToList();
            Methods = model.Methods?.Select(m => new DBMethodModel().MapDown(m)).ToList();
            Constructors = model.Constructors?.Select(c => new DBMethodModel().MapDown(c)).ToList();
            Properties = model.Properties?.Select(c => new DBPropertyModel().MapDown(c)).ToList();
        }

        private void FillType(DBTypeModel model, TypeModel typeModel)
        {
            typeModel.Name = model.Name;
            typeModel.IsExternal = model.IsExternal;
            typeModel.IsGeneric = model.IsGeneric;
            typeModel.Type = model.Type;
            typeModel.AssemblyName = model.AssemblyName;
            typeModel.Modifiers = model.Modifiers ?? new TypeModifiers();

            typeModel.BaseType = EmitType(model.BaseType);
            typeModel.DeclaringType = EmitType(model.DeclaringType);

            typeModel.NestedTypes = model.NestedTypes?.Select(EmitType).ToList();
            typeModel.GenericArguments = model.GenericArguments?.Select(EmitType).ToList();
            typeModel.ImplementedInterfaces = model.ImplementedInterfaces?.Select(EmitType).ToList();

            typeModel.Fields = model.Fields?.Select(g => g.MapUp(g)).ToList();
            typeModel.Methods = model.Methods?.Select(c => c.MapUp(c)).ToList();
            typeModel.Constructors = model.Constructors?.Select(c => c.MapUp(c)).ToList();
            typeModel.Properties = model.Properties?.Select(g => g.MapUp(g)).ToList();
        }

        #endregion

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
            if (model == null)
                return null;
            if (!DBTypes.ContainsKey(model.Name))
            {
                DBTypes.Add(model.Name, this);
                FillDBType(model);
            }
            return DBTypes[model.Name];
        }

        #endregion

    }
}
