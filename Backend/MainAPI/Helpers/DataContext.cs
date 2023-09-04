
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
