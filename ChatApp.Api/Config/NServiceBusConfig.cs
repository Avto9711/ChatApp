using ChatApp.Core.Models;
using ChatApp.Messages.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
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

            transport.Routing().RouteToEndpoint(typeof(RequestAaplCsv), nserviceBusConfig.DestinationName);



            //        var transport = endpointConfiguration
            //.UseTransport<SqlServerTransport>()
            //.ConnectionString(connection)
            //.DefaultSchema("dbo");

            //        transport
            //            .Routing()
            //            .RouteToEndpoint(typeof(ReponseAaplCsv), "ChatAppApi");

            //        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            //        var subscriptions = persistence.SubscriptionSettings();
            //        subscriptions.CacheFor(TimeSpan.FromMinutes(1));
            //        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
            //        persistence.ConnectionBuilder(
            //            connectionBuilder: () => new SqlConnection(connection));
            //        endpointConfiguration.EnableInstallers();

            endpointConfiguration.EnableInstallers();

            var instance = Endpoint.Start(endpointConfiguration).Result;
            services.AddSingleton((x) => instance);
        }
    }
}
