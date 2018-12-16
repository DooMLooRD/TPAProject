using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.DataModel;
using DataLayer.DataModel.Enums;

namespace DBData.Entities
{
    [Table("TypeModel")]
    public class DBTypeModel : BaseTypeModel
    {

        #region Constructor

        public DBTypeModel()
        {
            MethodGenericArguments = new HashSet<DBMethodModel>();
            TypeGenericArguments = new HashSet<DBTypeModel>();
            TypeImplementedInterfaces = new HashSet<DBTypeModel>();
            TypeNestedTypes = new HashSet<DBTypeModel>();
            Constructors=new List<DBMethodModel>();
            Fields=new List<DBParameterModel>();
            GenericArguments=new List<DBTypeModel>();
            ImplementedInterfaces=new List<DBTypeModel>();
            Methods=new List<DBMethodModel>();
            NestedTypes=new List<DBTypeModel>();
            Properties=new List<DBPropertyModel>();

        }

        #endregion

        #region Properties

        [Key, StringLength(150)]
        public override string Name { get; set; }

        public override string AssemblyName { get; set; }

        public override bool IsExternal { get; set; }

        public override bool IsGeneric { get; set; }

        public new DBTypeModel BaseType { get; set; }

        public override TypeEnum Type { get; set; }
        public new DBTypeModel DeclaringType { get; set; }

        public override TypeModifiers Modifiers { get; set; }

        public int? NamespaceId { get; set; }

        public new List<DBMethodModel> Constructors { get; set; }

        public new List<DBParameterModel> Fields { get; set; }

        public new List<DBTypeModel> GenericArguments { get; set; }

        public new List<DBTypeModel> ImplementedInterfaces { get; set; }

        public new List<DBMethodModel> Methods { get; set; }

        public new List<DBTypeModel> NestedTypes { get; set; }

        public new List<DBPropertyModel> Properties { get; set; }

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
