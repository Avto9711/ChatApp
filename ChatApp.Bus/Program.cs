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
            var endpointConfig = GetEndpointConfiguration("ChatAppBus");

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
        static EndpointConfiguration GetEndpointConfiguration(string endpointname)
        {
            var connection = appConfig.GetConnectionString("DefaultConnection");
            var endpointConfiguration = new EndpointConfiguration(endpointname);
            var transport = endpointConfiguration
                .UseTransport<SqlServerTransport>()
                .ConnectionString(connection);

            transport
                .Routing()
                .RouteToEndpoint(typeof(ReponseStockCsv), "ChatAppApi");

            endpointConfiguration.EnableInstallers();
            return endpointConfiguration;
        }
    }
}
