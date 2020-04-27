using ChatApp.Api.Config;
using ChatApp.Model.Contexts.ChatApp;
using ChatApp.Model.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.Extensions
{
    public static class APIExtensions
    {
        public async static void CreateUsers(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<IUserManagementService>();
                var context = scope.ServiceProvider.GetRequiredService<ChatAppDbContext>();
                var existUser = userManager.AnyUsers().Result;
                if (!existUser)
                {
                    await userManager.CreateUser(new Model.Entities.UserApplication { UserName = "user1", Name = "user 1" }, "P@$$w0rd36845");
                    await userManager.CreateUser(new Model.Entities.UserApplication { UserName = "user2", Name = "user 2" }, "P@$$w0rd36845");
                }

            }
        }

        public static void NServiceBusCreateDbInstance(this IApplicationBuilder app)
        {
            // Initiating instance to create db
            DependencyService.GetInstanceOf<IMessageSession>();
        }
    }
}
