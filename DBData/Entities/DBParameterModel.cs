using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.DataModel;

namespace DBData.Entities
{
    [Table("ParameterModel")]
    public class DBParameterModel : BaseParameterModel
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
        public override string Name { get; set; }

        public new DBTypeModel Type { get; set; }

        #endregion

        #region Inverse Properties

        public virtual ICollection<DBMethodModel> MethodParameters { get; set; }

        public virtual ICollection<DBTypeModel> TypeFields { get; set; }

        #endregion

    }
}
