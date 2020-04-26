using ChatApp.Api.Hubs;
using ChatApp.Api.Services;
using ChatApp.Core.Models;
using ChatApp.Messages.Commands;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.ObjectBuilder.MSDependencyInjection;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.Config
{
    public static class NServiceBusConfig
    {

        public static void AddNServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            var nserviceBusConfig = configuration.GetSection("NServiceBusConfig").Get<NServiceBusAppSettings>();
            var endpointConfiguration = new EndpointConfiguration(nserviceBusConfig.EndpointName);
            var transport = endpointConfiguration.UseTransport<SqlServerTransport>()
                                                    .ConnectionString(nserviceBusConfig.NServiceBusConnectionString);

            transport.Routing().RouteToEndpoint(typeof(RequestStockCSV), nserviceBusConfig.DestinationName);

            endpointConfiguration.UseContainer<StructureMapBuilder>(customizations => customizations.ExistingContainer(DependencyService.Container));

            services.AddSingleton<IMessageSession>(x => {
                return Endpoint.Start(endpointConfiguration)
                .GetAwaiter()
                .GetResult();
               }
             );
        }
    }
}
