using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.DataModel;

namespace DBData.Entities
{
    [Table("MethodModel")]
    public class DBMethodModel
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
     

    }
}
