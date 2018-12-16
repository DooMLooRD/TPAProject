using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.DataModel;

namespace DBData.Entities
{
    [Table("AssemblyModel")]
    [Export(typeof(BaseAssemblyModel))]
    public class DBAssemblyModel : BaseAssemblyModel
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public override string Name { get; set; }

        public new List<DBNamespaceModel> NamespaceModels { get; set; }

        #endregion

    }
}
