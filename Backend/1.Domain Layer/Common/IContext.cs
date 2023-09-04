using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DomainLayer.Common;

public interface IContext : IAsyncDisposable, IDisposable
{
    public DatabaseFacade Database { get; }

    public  DbSet<Module> Module { get;  }
    public  DbSet<ModuleField> ModuleField { get; }
    public  DbSet<ModuleDetail> ModuleDetail { get; }
    public  DbSet<ModuleDetailField> ModuleDetailField { get;  }
    public  DbSet<DataType> DataType { get; set; }
    public  DbSet<ControlType> ControlType { get;  }
    public  DbSet<User> User { get;  }
    public  DbSet<UserDetail> UserDetail { get; }
    public  DbSet<UserToken> UserToken { get; }


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}