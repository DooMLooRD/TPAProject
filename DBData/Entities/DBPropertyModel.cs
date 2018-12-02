using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessLogic.Model.Assembly;
using MEF;

namespace DBData.Entities
{
    [Table("PropertyModel")]
    public partial class DBPropertyModel : IModelMapper<PropertyModel, DBPropertyModel>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DBPropertyModel()
        {
            TypeProperties = new HashSet<DBTypeModel>();
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
        public virtual ICollection<DBTypeModel> TypeProperties { get; set; }

        public PropertyModel MapUp(DBPropertyModel model)
        {
            PropertyModel propertyModel = new PropertyModel();
            propertyModel.Name = model.Name;
            if (model.Type != null)
                propertyModel.Type = DBTypeModel.EmitType(model.Type);
            return propertyModel;
        }

        public DBPropertyModel MapDown(PropertyModel model)
        {
            Name = model.Name;
            Type = model.Type != null ? DBTypeModel.EmitDBType(model.Type) : null;
            return this;
        }
    }
}
