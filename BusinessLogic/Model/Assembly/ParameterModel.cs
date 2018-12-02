
namespace BusinessLogic.Model.Assembly
{
    public class ParameterModel
    {
        public string Name { get; set; }
        /// <summary>
        /// TypeModel of the parameter
        /// </summary>
        public TypeModel Type { get; set; }

        public ParameterModel()
        {
            
        }
        /// <summary>
        /// Constructor with name and TypeModel as params
        /// </summary>
        /// <param name="name"></param>
        /// <param name="typeModel"></param>
        public ParameterModel(string name, TypeModel typeModel)
        {
            Name = name;
            Type = typeModel;
        }



    }
}
