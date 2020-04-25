using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Core.BaseModel.BaseEntity;

namespace ChatApp.Model.Contexts.ChatApp
{
    public interface IChatAppDbContext : IDisposable
    {
        DatabaseFacade Database { get; }
        DbSet<T> GetDbSet<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        EntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
