using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessLogic.Model.Assembly;
using MEF;

namespace DBData.Entities
{
    [Table("AssemblyModel")]
    public class DBAssemblyModel : IModelMapper<AssemblyModel, DBAssemblyModel>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public List<DBNamespaceModel> NamespaceModels { get; set; }

        public AssemblyModel MapUp(DBAssemblyModel model)
        {
            AssemblyModel assemblyModel = new AssemblyModel();
            assemblyModel.Name = model.Name;
            assemblyModel.NamespaceModels=new List<NamespaceModel>();
            if(model.NamespaceModels != null)
            {
                foreach (DBNamespaceModel namespaceModel in model.NamespaceModels)
                {
                    assemblyModel.NamespaceModels.Add(new DBNamespaceModel().MapUp(namespaceModel));
                }
            }
            else
            {
                assemblyModel.NamespaceModels = null;
            }
            return assemblyModel;

        }

        public DBAssemblyModel MapDown(AssemblyModel model)
        {
            Name = model.Name;
            NamespaceModels = new List<DBNamespaceModel>();
            if (model.NamespaceModels != null)
            {
                foreach (NamespaceModel namespaceModel in model.NamespaceModels)
                {
                    NamespaceModels.Add(new DBNamespaceModel().MapDown(namespaceModel));
                }
            }
            else
            {
                NamespaceModels = null;
            }
            return this;
        }
    }
}
