using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessLogic.Model.Assembly;
using MEF;

namespace DBData.Entities
{
    [Table("ParameterModel")]
    public partial class DBParameterModel : IModelMapper<ParameterModel, DBParameterModel>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DBParameterModel()
        {
            MethodParameters = new HashSet<DBMethodModel>();
            TypeFields = new HashSet<DBTypeModel>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string TypeId { get; set; }
        [ForeignKey("TypeId")]
        public DBTypeModel Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBMethodModel> MethodParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DBTypeModel> TypeFields { get; set; }

        public ParameterModel MapUp(DBParameterModel model)
        {
            ParameterModel parameterModel = new ParameterModel();
            parameterModel.Name = model.Name;
            if (model.Type != null)
                parameterModel.Type = DBTypeModel.EmitType(model.Type);
            return parameterModel;
        }

        public DBParameterModel MapDown(ParameterModel model)
        {
            Name = model.Name;
            Type = model.Type != null ? DBTypeModel.EmitDBType(model.Type) : null;
            return this;
        }
    }
}
