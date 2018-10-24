using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Model;

namespace BusinessLogic.Reflection
{
    public class Reflector
    {
        public AssemblyModel AssemblyModel { get; private set; }
        public Reflector(Assembly assembly)
        {
            AssemblyModel = new AssemblyModel(assembly);
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
