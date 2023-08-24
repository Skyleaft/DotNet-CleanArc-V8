using DomainLayer.Common;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace MainAPI.Common
{
    public class MyDBContext : DbContext
    {
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModuleField> ModuleField { get; set; }
        public virtual DbSet<ModuleType> ModuleType { get; set; }

        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

    }
}
