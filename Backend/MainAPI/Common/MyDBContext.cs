using DomainLayer.Common;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace MainAPI.Common
{
    public class MyDBContext : DbContext
    {
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModuleField> ModuleField { get; set; }
        public virtual DbSet<ModuleDetail> ModuleDetail { get; set; }
        public virtual DbSet<ModuleDetailField> ModuleDetailField { get; set; }
        public virtual DbSet<DataType> DataType { get; set; }
        public virtual DbSet<ControlType> ControlType { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserDetail> UserDetail { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

    }
}
