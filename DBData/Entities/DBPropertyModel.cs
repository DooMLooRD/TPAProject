using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBData.Entities
{
    [Table("PropertyModel")]
    public class DBPropertyModel
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


    }
}
