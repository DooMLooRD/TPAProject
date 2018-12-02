using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;


namespace BusinessLogic.Model.Assembly
{
    public class PropertyModel
    {

        public string Name { get; set; }
        /// <summary>
        /// TypeModel of the Property
        /// </summary>
        public TypeModel Type { get; set; }

        public PropertyModel()
        {
            
        }
        /// <summary>
        /// Constructor with name and TypeModel as params
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propertyType"></param>
        public PropertyModel(string name, TypeModel propertyType)
        {
            Name = name;
            Type = propertyType;
        }

        /// <summary>
        /// Emits PropertyModel collection from PropertyInfo collection
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<PropertyModel> EmitProperties(Type type)
        {
            List<PropertyInfo> props = type
                .GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                               BindingFlags.Static | BindingFlags.Instance).ToList();

            return props.Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible())
                .Select(t => new PropertyModel(t.Name, TypeModel.EmitReference(t.PropertyType))).ToList(); 
        }

    }
}
