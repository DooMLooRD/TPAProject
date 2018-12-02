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
    public partial class DBTypeModel : IModelMapper<TypeModel, DBTypeModel>
    {
        public static Dictionary<string, DBTypeModel> DBTypes = new Dictionary<string, DBTypeModel>();
        public static Dictionary<string, TypeModel> Types = new Dictionary<string, TypeModel>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        [Key]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string AssemblyName { get; set; }

        public bool IsExternal { get; set; }

        public bool IsGeneric { get; set; }

        public DBTypeModel BaseType { get; set; }
        [InverseProperty("BaseType")]
        public virtual ICollection<DBTypeModel> TypeBaseTypes { get; set; }

        public TypeEnum Type { get; set; }

        public DBTypeModel DeclaringType { get; set; }

        [InverseProperty("DeclaringType")]
        public virtual ICollection<DBTypeModel> TypeDeclaringTypes { get; set; }

        public TypeModifiers Modifiers { get; set; }

        public int? NamespaceId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBMethodModel> MethodGenericArguments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBMethodModel> Constructors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBParameterModel> Fields { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBTypeModel> TypeGenericArguments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBTypeModel> GenericArguments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBTypeModel> TypeImplementedInterfaces { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBTypeModel> ImplementedInterfaces { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBMethodModel> Methods { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBTypeModel> TypeNestedTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBTypeModel> NestedTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBPropertyModel> Properties { get; set; }




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
            if (model.AssemblyName != null)
                AssemblyName = model.AssemblyName;
            BaseType = model.BaseType != null ? EmitDBType(model.BaseType) : null;
            Constructors = model.Constructors != null ? model.Constructors.Select(c => new DBMethodModel().MapDown(c)).ToList() : new List<DBMethodModel>();
            DeclaringType = model.DeclaringType != null ? EmitDBType(model.DeclaringType) : null;
            Fields = model.Fields != null ? model.Fields?.Select(c => new DBParameterModel().MapDown(c)).ToList() : new List<DBParameterModel>();
            GenericArguments = model.GenericArguments != null ? model.GenericArguments.Select(c => EmitDBType(c)).ToList() : new List<DBTypeModel>();
            ImplementedInterfaces = model.ImplementedInterfaces != null ? model.ImplementedInterfaces.Select(c => EmitDBType(c)).ToList() : new List<DBTypeModel>();
            Methods = model.Methods != null ? model.Methods.Select(m => new DBMethodModel().MapDown(m)).ToList() : new List<DBMethodModel>();
            Modifiers = model.Modifiers ?? new TypeModifiers();
            NestedTypes = model.NestedTypes != null ? model.NestedTypes.Select(c => EmitDBType(c)).ToList() : new List<DBTypeModel>();
            Properties = model.Properties != null ? model.Properties.Select(c => new DBPropertyModel().MapDown(c)).ToList() : new List<DBPropertyModel>();
        }
        private void FillType(DBTypeModel model, TypeModel typeModel)
        {
            typeModel.Name = model.Name;
            typeModel.IsExternal = model.IsExternal;
            typeModel.IsGeneric = model.IsGeneric;
            typeModel.Type = model.Type;
            if (model.AssemblyName != null)
                typeModel.AssemblyName = model.AssemblyName;
            if (model.BaseType != null)
                typeModel.BaseType = EmitType(model.BaseType);
            if (model.Constructors != null)
                typeModel.Constructors = model.Constructors.Select(c => c.MapUp(c)).ToList();
            if (model.DeclaringType != null)
                typeModel.DeclaringType = EmitType(model.DeclaringType);
            if (model.Fields != null)
                typeModel.Fields = model.Fields?.Select(g => g.MapUp(g)).ToList();
            if (model.GenericArguments != null)
                typeModel.GenericArguments = model.GenericArguments.Select(EmitType).ToList();
            if (model.ImplementedInterfaces != null)
                typeModel.ImplementedInterfaces = model.ImplementedInterfaces.Select(EmitType).ToList();
            if (model.Methods != null)
                typeModel.Methods = model.Methods.Select(c => c.MapUp(c)).ToList();
            if (model.Modifiers != null)
                typeModel.Modifiers = model.Modifiers;
            if (model.NestedTypes != null)
                typeModel.NestedTypes = model.NestedTypes.Select(EmitType).ToList();
            if (model.Properties != null)
                typeModel.Properties = model.Properties.Select(g => g.MapUp(g)).ToList();
        }


        public TypeModel MapUp(DBTypeModel model)
        {
            TypeModel typeModel = new TypeModel();
            if (!Types.ContainsKey(model.Name))
            {
                Types.Add(model.Name, typeModel);
                FillType(model, typeModel);
            }
            return Types[model.Name];

        }

        public DBTypeModel MapDown(TypeModel model)
        {
            if (!DBTypes.ContainsKey(model.Name))
            {
                DBTypes.Add(model.Name, this);
                FillDBType(model);
            }
            return DBTypes[model.Name];
        }
    }
}
