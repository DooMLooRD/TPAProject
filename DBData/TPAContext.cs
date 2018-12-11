using DBData.Entities;
using System.Data.Entity;

namespace DBData
{


    public  class TPADBContext : DbContext
    {
        private const string connectionString =
            @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=DBData.TPAContext;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        public TPADBContext()
            : base(connectionString)
        {
        }

        public virtual DbSet<DBAssemblyModel> AssemblyModel { get; set; }
        public virtual DbSet<DBMethodModel> MethodModel { get; set; }
        public virtual DbSet<DBNamespaceModel> NamespaceModel { get; set; }
        public virtual DbSet<DBParameterModel> ParameterModel { get; set; }
        public virtual DbSet<DBPropertyModel> PropertyModel { get; set; }
        public virtual DbSet<DBTypeModel> TypeModel { get; set; }
        public virtual DbSet<LogEntity> Log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
