using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBData.Entities
{
    [Table("NamespaceModel")]
    public class DBNamespaceModel 
    {

        #region Properties

        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        public List<DBTypeModel> Types { get; set; }

        #endregion
  

    }

}
