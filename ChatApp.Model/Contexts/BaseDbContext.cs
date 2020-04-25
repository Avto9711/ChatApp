using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Core.BaseModel.BaseEntity;
using ChatApp.Core.User;
using ChatApp.Model.Extensions;
using ChatApp.Model.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ChatApp.Model.Contexts
{
    public abstract class BaseDbContext : IdentityDbContext<UserApplication>
    {
        public BaseDbContext(DbContextOptions options) : base(options) { }

        private void SetAuditEntities()
        {
            foreach (var entry in ChangeTracker.Entries<IBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        if (entry.Entity.Id > 0)
                        {
                            entry.State = EntityState.Modified;
                            goto case EntityState.Modified;
                        }

                        entry.Entity.Deleted = false;
                        entry.Entity.CreatedBy = CurrentUser.UserName;
                        if (!entry.Entity.CreatedDate.HasValue)
                            entry.Entity.CreatedDate = DateTimeOffset.Now;
                        break;
                    case EntityState.Modified:
                        entry.Property(x => x.CreatedDate).IsModified = false;
                        entry.Property(x => x.CreatedBy).IsModified = false;
                        entry.Entity.UpdatedDate = DateTimeOffset.Now;
                        entry.Entity.UpdatedBy = CurrentUser.UserName;

                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.Deleted = true;
                        goto case EntityState.Modified;

                    default:
                        goto case EntityState.Modified;
                }
            }
        }
        private async Task<int> BeforeSaveAsync(Func<Task<int>> action)
        {
            SetAuditEntities();
            return await action.Invoke();
        }
        private int BeforeSave(Func<int> action)
        {
            SetAuditEntities();
            return action.Invoke();
        }
        public override int SaveChanges()
        {
            return BeforeSave(() => base.SaveChanges());
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return BeforeSave(() => base.SaveChanges(acceptAllChangesOnSuccess));
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await BeforeSaveAsync(() => base.SaveChangesAsync(cancellationToken));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IBaseEntity).IsAssignableFrom(type.ClrType))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }
        }
    }
}
