using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Model
{
    public class PropertyModel : BaseModel
    {
        /// <summary>
        /// TypeModel of the Property
        /// </summary>
        public TypeModel Type { get; set; }

        /// <summary>
        /// Constructor with name and TypeModel as params
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propertyType"></param>
        public PropertyModel(string name, TypeModel propertyType) : base(name)
        {
            Type = propertyType;
        }

        /// <summary>
        /// Emits PropertyModel collection from PropertyInfo collection
        /// </summary>
        /// <param name="props"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyModel> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                select new PropertyModel(prop.Name, TypeModel.EmitReference(prop.PropertyType));
        }
    }
}
