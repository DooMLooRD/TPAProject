using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.DataModel;

namespace DBData.Entities
{
    [Table("NamespaceModel")]
    public class DBNamespaceModel : BaseNamespaceModel
    {

        #region Properties

        public int Id { get; set; }

        [Required, StringLength(150)]
        public override string Name { get; set; }

        public new List<DBTypeModel> Types { get; set; }

        #endregion
  

    }

}
