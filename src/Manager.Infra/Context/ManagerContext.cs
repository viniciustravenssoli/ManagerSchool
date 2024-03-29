using System.Reflection;
using Manager.Domain.Entities;
using Manager.Infra.Mappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Context
{
    public class ManagerContext : DbContext
    {
        public ManagerContext()
        { }

        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-7JPLA67\SQLEXPRESS1;Database=ManagerSchool;Trusted_Connection=True;");
        }

        //    public virtual DbSet<User> Users { get; set; }

        // public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Teacher> Teachers { get; set; }

        public virtual DbSet<Class> Classes { get; set; }

        public virtual DbSet<Boletim> Boletins { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}