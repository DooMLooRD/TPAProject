using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.DataModel;
using DataLayer.DataModel.Enums;

namespace DBData.Entities
{
    [Table("TypeModel")]
    public class DBTypeModel 
    {

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

    }
}
