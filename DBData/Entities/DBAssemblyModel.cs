using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessLogic.Model.Assembly;
using MEF;

namespace DBData.Entities
{
    [Table("AssemblyModel")]
    public class DBAssemblyModel : IModelMapper<AssemblyModel, DBAssemblyModel>
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public List<DBNamespaceModel> NamespaceModels { get; set; }

        #endregion

        #region IModelMapper

        public AssemblyModel MapUp(DBAssemblyModel model)
        {
            AssemblyModel assemblyModel = new AssemblyModel();
            assemblyModel.Name = model.Name;
            assemblyModel.NamespaceModels = model.NamespaceModels?.Select(t => new DBNamespaceModel().MapUp(t)).ToList();
            return assemblyModel;
        }

        public DBAssemblyModel MapDown(AssemblyModel model)
        {
            Name = model.Name;
            NamespaceModels = model.NamespaceModels?.Select(t=> new DBNamespaceModel().MapDown(t)).ToList();
            return this;
        }

        #endregion

    }
}
