using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using DataLayer;
using DataLayer.DataModel;
using DBData.Entities;

namespace DBData
{
    [Export(typeof(ISerializer))]
    public class DatabaseSerializer : ISerializer
    {

        public void Save(BaseAssemblyModel _object, string path)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<TPADBContext>());
            using (TPADBContext context = new TPADBContext())
            {
                DBAssemblyModel assemblyModel = (DBAssemblyModel)_object;
                context.AssemblyModel.Add(assemblyModel);
                context.SaveChanges();
            }
        }

        public BaseAssemblyModel Read(string path)
        {
            using (TPADBContext context = new TPADBContext())
            {
                context.ParameterModel
                    .Include(p => p.Type)
                    .Include(p => p.TypeFields)
                    .Include(p => p.MethodParameters)
                    .Load();
                context.TypeModel
                    .Include(t => t.Constructors)
                    .Include(t => t.BaseType)
                    .Include(t => t.DeclaringType)
                    .Include(t => t.Fields)
                    .Include(t => t.ImplementedInterfaces)
                    .Include(t => t.GenericArguments)
                    .Include(t => t.Methods)
                    .Include(t => t.NestedTypes)
                    .Include(t => t.Properties)
                    .Include(t => t.TypeGenericArguments)
                    .Include(t => t.TypeImplementedInterfaces)
                    .Include(t => t.TypeNestedTypes)
                    .Include(t => t.MethodGenericArguments)
                    .Load();
                context.MethodModel
                    .Include(m => m.GenericArguments)
                    .Include(m => m.Parameters)
                    .Include(m => m.ReturnType)
                    .Include(m => m.TypeConstructors)
                    .Include(m => m.TypeMethods)
                    .Load();
                context.PropertyModel
                    .Include(p => p.Type)
                    .Include(p => p.TypeProperties)
                    .Load();
                context.NamespaceModel
                    .Include(n => n.Types)
                    .Load();

                DBAssemblyModel dbAssemblyModel = context.AssemblyModel
                    .Include(a => a.NamespaceModels)
                    .ToList()[0];
                return dbAssemblyModel;
            }
        }

        private void ClearDB()
        {
            using (TPADBContext context = new TPADBContext())
            {
                context.AssemblyModel.RemoveRange(context.AssemblyModel);
                context.NamespaceModel.RemoveRange(context.NamespaceModel);
                context.TypeModel.RemoveRange(context.TypeModel);
                context.ParameterModel.RemoveRange(context.ParameterModel);
                context.PropertyModel.RemoveRange(context.PropertyModel);
            }
        }
    }
}
