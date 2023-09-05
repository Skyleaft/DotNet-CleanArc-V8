
using DomainLayer.Common;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace MainAPI.Helpers
{
    public class DataContext : DbContext,IContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //SQL Server Connection
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultSQLCON"));
            //PGSQL Connection
            options.UseNpgsql(Configuration.GetConnectionString("PGSQLConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role[] {
                new Role{Id=1,Level=1,Name="Super Admin"},
            });
            modelBuilder.Entity<User>().HasData(new User[] {
                new User{Id=1,Username="admin",Password="admin123",RoleId=1 }
            });
            modelBuilder.Entity<UserDetail>().HasData(new UserDetail[] {
                new UserDetail{Id=1,Name="Administrator" , UserId=1 }
            });
        }

        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModuleField> ModuleField { get; set; }
        public virtual DbSet<ModuleDetail> ModuleDetail { get; set; }
        public virtual DbSet<ModuleDetailField> ModuleDetailField { get; set; }
        public virtual DbSet<DataType> DataType { get; set; }
        public virtual DbSet<ControlType> ControlType { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserDetail> UserDetail { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }

    }
}
