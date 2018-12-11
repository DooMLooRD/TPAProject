using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.DataModel;

namespace DBData.Entities
{
    [Table("AssemblyModel")]
    public class DBAssemblyModel : IAssemblyModel
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public List<DBNamespaceModel> NamespaceModels { get; set; }

        #endregion

    }
}
