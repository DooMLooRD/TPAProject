using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessLogic.Model.Assembly;
using MEF;

namespace DBData.Entities
{
    [Table("MethodModel")]
    public partial class DBMethodModel : IModelMapper<MethodModel, DBMethodModel>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DBMethodModel()
        {
            GenericArguments = new HashSet<DBTypeModel>();
            Parameters = new HashSet<DBParameterModel>();
            TypeConstructors = new HashSet<DBTypeModel>();
            TypeMethods = new HashSet<DBTypeModel>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public bool Extension { get; set; }
        public MethodModifiers Modifiers { get; set; }

        [StringLength(150)]
        public string ReturnTypeId { get; set; }
        [ForeignKey("ReturnTypeId")]
        public virtual DBTypeModel ReturnType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBTypeModel> GenericArguments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBParameterModel> Parameters { get; set; }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBTypeModel> TypeConstructors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBTypeModel> TypeMethods { get; set; }

        public MethodModel MapUp(DBMethodModel model)
        {
            MethodModel methodModel = new MethodModel();
            methodModel.Name = model.Name;
            methodModel.Extension = model.Extension;
            if (model.GenericArguments != null)
                methodModel.GenericArguments = model.GenericArguments.Select(c => DBTypeModel.EmitType(c)).ToList();
            methodModel.Modifiers = model.Modifiers;
            if (model.Parameters != null)
                methodModel.Parameters = model.Parameters.Select(p => p.MapUp(p)).ToList();
            if (model.ReturnType != null)
                methodModel.ReturnType = DBTypeModel.EmitType(model.ReturnType);
            return methodModel;
        }

        public DBMethodModel MapDown(MethodModel model)
        {
            Name = model.Name;
            Extension = model.Extension;
            GenericArguments = model.GenericArguments != null ? model.GenericArguments.Select(c => DBTypeModel.EmitDBType(c)).ToList() : new List<DBTypeModel>();
            Modifiers = model.Modifiers ?? new MethodModifiers();
            Parameters = model.Parameters != null ? model.Parameters.Select(p => new DBParameterModel().MapDown(p)).ToList() : new List<DBParameterModel>();
            ReturnType = model.ReturnType != null ? DBTypeModel.EmitDBType(model.ReturnType) : null;
            return this;
        }
    }
}
