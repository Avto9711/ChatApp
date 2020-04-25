using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ChatApp.Model.Contexts.ChatApp;
using ChatApp.Model.UnitOfWorks;
using ChatApp.Model.UnitOfWorks.ChatApp;
using ChatApp.Model.Services;

namespace ChatApp.Model.IoC
{
    public static class ModelRegistry
    {
        public static void AddModelRegistry(this IServiceCollection services)
        {
            services.AddTransient<IChatAppDbContext, ChatAppDbContext>();
            services.AddScoped<IUnitOfWork<IChatAppDbContext>, ChatAppUnitOfWork>();
            services.AddTransient<IUserManagementService, UserManagementService>();

        }
    }
}
