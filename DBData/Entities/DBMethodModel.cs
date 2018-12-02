using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessLogic.Model.Assembly;
using MEF;

namespace DBData.Entities
{
    [Table("MethodModel")]
    public class DBMethodModel : IModelMapper<MethodModel, DBMethodModel>
    {

        #region Constructor

        public DBMethodModel()
        {
            GenericArguments = new HashSet<DBTypeModel>();
            Parameters = new HashSet<DBParameterModel>();
            TypeConstructors = new HashSet<DBTypeModel>();
            TypeMethods = new HashSet<DBTypeModel>();
        }

        #endregion

        #region Propeties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public bool Extension { get; set; }
        public MethodModifiers Modifiers { get; set; }

        public virtual DBTypeModel ReturnType { get; set; }
        public virtual ICollection<DBTypeModel> GenericArguments { get; set; }
        public virtual ICollection<DBParameterModel> Parameters { get; set; }

        #endregion

        #region Inverse Properties

        public virtual ICollection<DBTypeModel> TypeConstructors { get; set; }

        public virtual ICollection<DBTypeModel> TypeMethods { get; set; }

        #endregion

        #region IModelMapper

        public MethodModel MapUp(DBMethodModel model)
        {
            MethodModel methodModel = new MethodModel();
            methodModel.Name = model.Name;
            methodModel.Extension = model.Extension;
            methodModel.GenericArguments = model.GenericArguments?.Select(c => DBTypeModel.EmitType(c)).ToList();
            methodModel.Modifiers = model.Modifiers ?? new MethodModifiers();
            methodModel.Parameters = model.Parameters?.Select(p => p.MapUp(p)).ToList();
            methodModel.ReturnType = DBTypeModel.EmitType(model.ReturnType);
            return methodModel;
        }

        public DBMethodModel MapDown(MethodModel model)
        {
            Name = model.Name;
            Extension = model.Extension;
            GenericArguments = model.GenericArguments?.Select(c => DBTypeModel.EmitDBType(c)).ToList();
            Modifiers = model.Modifiers ?? new MethodModifiers();
            Parameters = model.Parameters?.Select(p => new DBParameterModel().MapDown(p)).ToList();
            ReturnType = DBTypeModel.EmitDBType(model.ReturnType);
            return this;
        }

        #endregion

    }
}
