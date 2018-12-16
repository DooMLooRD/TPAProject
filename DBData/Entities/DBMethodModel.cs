using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.DataModel;

namespace DBData.Entities
{
    [Table("MethodModel")]
    public class DBMethodModel : BaseMethodModel
    {

        #region Constructor

        public DBMethodModel()
        {
            GenericArguments= new List<DBTypeModel>();
            Parameters=new List<DBParameterModel>();
            TypeConstructors = new HashSet<DBTypeModel>();
            TypeMethods = new HashSet<DBTypeModel>();
        }

        #endregion

        #region Propeties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public override string Name { get; set; }

        public override bool Extension { get; set; }
        public override MethodModifiers Modifiers { get; set; }

        public new DBTypeModel ReturnType { get; set; }
        public new List<DBTypeModel> GenericArguments { get; set; }
        public new List<DBParameterModel> Parameters { get; set; }

        #endregion

        #region Inverse Properties

        public virtual ICollection<DBTypeModel> TypeConstructors { get; set; }

        public virtual ICollection<DBTypeModel> TypeMethods { get; set; }

        #endregion
     

    }
}
