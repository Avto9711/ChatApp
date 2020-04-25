using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Core.BaseModel.BaseEntity;
using ChatApp.Model.Contexts.ChatApp;
using ChatApp.Model.Repositories;

namespace ChatApp.Model.UnitOfWorks.ChatApp
{
    public class ChatAppUnitOfWork : IUnitOfWork<IChatAppDbContext>
    {
        public IChatAppDbContext context { get; set; }
        public readonly IServiceProvider _serviceProvider;

        public ChatAppUnitOfWork(IServiceProvider serviceProvider, IChatAppDbContext context)
        {
            _serviceProvider = serviceProvider;
            this.context = context;
        }

        public async Task<int> Commit()
        {
            var result = await context.SaveChangesAsync();
            return result;
        }


        public void Dispose()
        {
            context.Dispose();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity
        {
            return (_serviceProvider.GetService(typeof(TEntity)) ?? new BaseRepository<TEntity>(this)) as IRepository<TEntity>;
        }
    }
}
