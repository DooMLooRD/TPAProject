using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessLogic.Model.Assembly;
using MEF;

namespace DBData.Entities
{
    [Table("PropertyModel")]
    public class DBPropertyModel : IModelMapper<PropertyModel, DBPropertyModel>
    {

        #region Constructor

        public DBPropertyModel()
        {
            TypeProperties = new HashSet<DBTypeModel>();
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public DBTypeModel Type { get; set; }

        #endregion

        #region Inverse Properties

        public virtual ICollection<DBTypeModel> TypeProperties { get; set; }

        #endregion

        #region IModelMapper

        public PropertyModel MapUp(DBPropertyModel model)
        {
            PropertyModel propertyModel = new PropertyModel();
            propertyModel.Name = model.Name;
            propertyModel.Type = DBTypeModel.EmitType(model.Type);
            return propertyModel;
        }

        public DBPropertyModel MapDown(PropertyModel model)
        {
            Name = model.Name;
            Type = DBTypeModel.EmitDBType(model.Type);
            return this;
        }

        #endregion

    }
}
