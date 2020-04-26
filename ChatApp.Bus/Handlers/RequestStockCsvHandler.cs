using ChatApp.Bot.Config;
using ChatApp.Bot.Models;
using ChatApp.Messages.Commands;
using CsvHelper;
using Microsoft.Extensions.Configuration;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Bus.Handlers
{
    public class RequestStockCsvHandler : IHandleMessages<RequestStockCSV>
    {
        static ILog log = LogManager.GetLogger<RequestStockCsvHandler>();
        private readonly HttpClient _client = new HttpClient();
        private static IConfigurationRoot appConfig;

        public RequestStockCsvHandler()
        {
            appConfig = Program.GetConfigurationRoot();

        }
        public async Task Handle(RequestStockCSV message, IMessageHandlerContext context)
        {
            var stockConfig = appConfig.GetSection("StockConfig").Get<StockConfig>();
            var stockUrl = stockConfig.ServiceUrl.Replace("{{stock}}", message.Stock);
            try
            {
                using (var response = await _client.GetAsync(stockUrl))
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                        var lin = await response.Content.ReadAsStringAsync();
                        var x = stream;
                        var y = reader;
                        //reader.Re
                        csv.Configuration.RegisterClassMap<StockCsvMap>();
                        csv.Configuration.Delimiter = ",";
                        var stock = csv.GetRecords<StockCsv>()
                            .ToList().FirstOrDefault();

                        var botMessage = string.Format("{0} quote is ${1} per share", message.Stock.ToUpper(), stock.Close);

                        await context.Send(new ReponseStockCsv { Id = Guid.NewGuid().ToString(), BotMessage = botMessage, ChatRoomId = message.ChatRoomId });
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw ex;
            }


        }
    }
}
