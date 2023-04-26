using Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Context
{
    public class ManagerContext : IdentityDbContext
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

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        //    protected override void OnModelCreating(ModelBuilder builder)
        //    {
        //     builder.ApplyConfiguration(new UserMap());
        //    }
    }
}