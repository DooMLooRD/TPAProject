using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.DataModel;

namespace DBData.Entities
{
    [Table("PropertyModel")]
    public class DBPropertyModel : BasePropertyModel
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
        public override string Name { get; set; }

        public new DBTypeModel Type { get; set; }

        #endregion

        #region Inverse Properties

        public virtual ICollection<DBTypeModel> TypeProperties { get; set; }

        #endregion


    }
}
