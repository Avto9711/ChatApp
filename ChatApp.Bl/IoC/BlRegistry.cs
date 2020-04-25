using ChatApp.Bl.Services.ChatMessage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Bl.IoC
{
    public static class BlRegistry
    {
        public static void AddBlRegistry(this IServiceCollection services)
        {
            services.AddTransient<IChatMessageService, ChatMessageService>();
        }
    }
}
