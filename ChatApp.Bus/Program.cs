using ChatApp.Core.Models;
using ChatApp.Messages.Commands;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Persistence.Sql;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ChatApp.Bus
{
    class Program
    {
        private static IConfigurationRoot appConfig;

        static async Task Main(string[] args)
        {
            Console.Title = "ChatAppBus";

            appConfig = GetConfigurationRoot();
            var nsbconfig = appConfig.GetSection("NServiceBusConfig").Get<NServiceBusAppSettings>();

            var endpointConfig = GetEndpointConfiguration(nsbconfig.EndpointName, nsbconfig.NServiceBusConnectionString, nsbconfig.DestinationName);

            var endpointInstance = await Endpoint.Start(endpointConfig).ConfigureAwait(false);

            Console.WriteLine("\r\nPress enter key to stop program\r\n");
            Console.ReadKey();
            await endpointInstance.Stop().ConfigureAwait(false);


        }
       internal static IConfigurationRoot GetConfigurationRoot()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true);

            return builder.Build();
        }
        static EndpointConfiguration GetEndpointConfiguration(string endpointName, string connectionString, string destinationName)
        {
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            var transport = endpointConfiguration
                .UseTransport<SqlServerTransport>()
                .ConnectionString(connectionString);

            transport
                .Routing()
                .RouteToEndpoint(typeof(ReponseStockCsv), destinationName);

            endpointConfiguration.EnableInstallers();
            return endpointConfiguration;
        }
    }
}
