using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessLogic.Model.Assembly;
using MEF;

namespace DBData.Entities
{
    [Table("ParameterModel")]
    public class DBParameterModel : IModelMapper<ParameterModel, DBParameterModel>
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

        #region IModelMapper

        public ParameterModel MapUp(DBParameterModel model)
        {
            ParameterModel parameterModel = new ParameterModel();
            parameterModel.Name = model.Name;
            parameterModel.Type = DBTypeModel.EmitType(model.Type);
            return parameterModel;
        }

        public DBParameterModel MapDown(ParameterModel model)
        {
            Name = model.Name;
            Type = DBTypeModel.EmitDBType(model.Type);
            return this;
        }

        #endregion

    }
}
