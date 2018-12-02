using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessLogic.Model.Assembly;
using MEF;

namespace DBData.Entities
{


    [Table("NamespaceModel")]
    public partial class DBNamespaceModel : IModelMapper<NamespaceModel, DBNamespaceModel>
    {
        public int Id { get; set; }

        [Required] [StringLength(150)] public string Name { get; set; }

        public List<DBTypeModel> Types { get; set; }
        [StringLength(150)] public string AssemblyId { get; set; }

        public NamespaceModel MapUp(DBNamespaceModel model)
        {
            NamespaceModel namespaceModel = new NamespaceModel();
            namespaceModel.Name = model.Name;
            if (model.Types != null)
                namespaceModel.Types = model.Types.Select(c=>DBTypeModel.EmitType(c)).ToList();
            return namespaceModel;
        }

        public DBNamespaceModel MapDown(NamespaceModel model)
        {
            Name = model.Name;
            Types = new List<DBTypeModel>();
            foreach (TypeModel type in model.Types)
            {
                Types.Add(new DBTypeModel().MapDown(type));
            }

            return this;
        }
    }

}
