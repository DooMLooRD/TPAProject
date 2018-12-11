using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBData.Entities
{
    [Table("ParameterModel")]
    public class DBParameterModel 
    {
   
        #region Constructor

        public DBParameterModel()
        {
            MethodParameters = new HashSet<DBMethodModel>();
            TypeFields = new HashSet<DBTypeModel>();
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

        public virtual ICollection<DBMethodModel> MethodParameters { get; set; }

        public virtual ICollection<DBTypeModel> TypeFields { get; set; }

        #endregion

    }
}
