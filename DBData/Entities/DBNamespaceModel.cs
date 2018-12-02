using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessLogic.Model.Assembly;
using MEF;

namespace DBData.Entities
{
    [Table("NamespaceModel")]
    public class DBNamespaceModel : IModelMapper<NamespaceModel, DBNamespaceModel>
    {

        #region Properties

        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        public List<DBTypeModel> Types { get; set; }

        #endregion

        #region IModelMapper

        public NamespaceModel MapUp(DBNamespaceModel model)
        {
            NamespaceModel namespaceModel = new NamespaceModel();
            namespaceModel.Name = model.Name;
            namespaceModel.Types = model.Types?.Select(c => DBTypeModel.EmitType(c)).ToList();
            return namespaceModel;
        }

        public DBNamespaceModel MapDown(NamespaceModel model)
        {
            Name = model.Name;
            Types = model.Types?.Select(t=> new DBTypeModel().MapDown(t)).ToList();
            return this;
        }

        #endregion

    }

}
