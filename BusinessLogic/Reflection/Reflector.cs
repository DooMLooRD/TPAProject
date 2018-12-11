using System.ComponentModel.Composition;
using System.Reflection;
using BusinessLogic.Model.Assembly;

namespace BusinessLogic.Reflection
{
    public class Reflector
    {
        public AssemblyModel AssemblyModel { get; private set; }
        public Reflector(Assembly assembly)
        {
            AssemblyModel = new AssemblyModel(assembly);
        }
        public Reflector(AssemblyModel assemblyModel)
        {
            AssemblyModel = assemblyModel;
        }

        public Reflector(string assemblyPath)
        {
            if (string.IsNullOrEmpty(assemblyPath))
                throw new System.ArgumentNullException();
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            AssemblyModel = new AssemblyModel(assembly);
        }
    }
}
