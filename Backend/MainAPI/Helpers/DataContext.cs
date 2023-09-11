
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
                new Role{Id=2,Level=2,Name="Super User"},
                new Role{Id=3,Level=3,Name="User"},
            });
            modelBuilder.Entity<User>().HasData(new User[] {
                new User{Id=1,Username="sysadmin",Password="admin123",RoleId=1,CreatedBy="sysadmin",CreatedAt=DateTime.Now },
                new User{Id=2,Username="user",Password="userqweqwe",RoleId=3,CreatedBy="sysadmin",CreatedAt=DateTime.Now }
            });
            modelBuilder.Entity<UserDetail>().HasData(new UserDetail[] {
                new UserDetail{Id=1,Name="Administrator" , UserId=1 },
                new UserDetail{Id=2,Name="User" , UserId=2}
            });
            modelBuilder.Entity<DataType>().HasData(new DataType[] {
                new DataType{Id=1,Name="VARCHAR"},
                new DataType{Id=2,Name="SMALLINT"},
                new DataType{Id=3,Name="INT"},
                new DataType{Id=4,Name="BIGINT"},
                new DataType{Id=5,Name="FLOAT"},
                new DataType{Id=6,Name="BOOLEAN"},
                new DataType{Id=7,Name="DATETIME"},
                new DataType{Id=8,Name="MONEY"},
                new DataType{Id=9,Name="IDENTITY"},
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
