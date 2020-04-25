using ChatApp.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.IoC
{
    public static class ApiRegistry
    {
        public static void AddApiRegistry(this IServiceCollection services)
        {
            services.AddTransient<IJwtService, JwtService>();
        }
    }
}
