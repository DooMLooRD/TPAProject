using DBData.Entities;

namespace DBData
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

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
            modelBuilder.Entity<DBMethodModel>()
                .HasMany(e => e.GenericArguments)
                .WithMany(e => e.MethodGenericArguments)
                .Map(m => m.ToTable("MethodGenericArguments").MapLeftKey("MethodId").MapRightKey("GenericArgumentId"));

            modelBuilder.Entity<DBMethodModel>()
                .HasMany(e => e.Parameters)
                .WithMany(e => e.MethodParameters)
                .Map(m => m.ToTable("MethodParameters").MapLeftKey("MethodId").MapRightKey("ParameterId"));

            modelBuilder.Entity<DBMethodModel>()
                .HasMany(e => e.TypeConstructors)
                .WithMany(e => e.Constructors)
                .Map(m => m.ToTable("TypeConstructors").MapLeftKey("ContructorId").MapRightKey("TypeId"));

            modelBuilder.Entity<DBMethodModel>()
                .HasMany(e => e.TypeMethods)
                .WithMany(e => e.Methods)
                .Map(m => m.ToTable("TypeMethods").MapLeftKey("MethodId").MapRightKey("TypeId"));

            modelBuilder.Entity<DBParameterModel>()
                .HasMany(e => e.TypeFields)
                .WithMany(e => e.Fields)
                .Map(m => m.ToTable("TypeFields").MapLeftKey("FieldId").MapRightKey("TypeId"));

            modelBuilder.Entity<DBPropertyModel>()
                .HasMany(e => e.TypeProperties)
                .WithMany(e => e.Properties)
                .Map(m => m.ToTable("TypeProperties").MapLeftKey("PropertyId").MapRightKey("TypeId"));

            modelBuilder.Entity<DBTypeModel>()
                .HasMany(e => e.TypeGenericArguments)
                .WithMany(e => e.GenericArguments)
                .Map(m => m.ToTable("TypeGenericArguments").MapLeftKey("GenericArgumentId").MapRightKey("TypeId"));

            modelBuilder.Entity<DBTypeModel>()
                .HasMany(e => e.TypeImplementedInterfaces)
                .WithMany(e => e.ImplementedInterfaces)
                .Map(m => m.ToTable("TypeImplementedInterfaces").MapLeftKey("ImplementedInterface").MapRightKey("TypeId"));

            modelBuilder.Entity<DBTypeModel>()
                .HasMany(e => e.TypeNestedTypes)
                .WithMany(e => e.NestedTypes)
                .Map(m => m.ToTable("TypeNestedTypes").MapLeftKey("NestedType").MapRightKey("TypeId"));
        }
    }
}
