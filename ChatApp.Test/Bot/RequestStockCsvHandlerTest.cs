using ChatApp.Bus.Handlers;
using ChatApp.Messages.Commands;
using NServiceBus.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatApp.Test.Bot
{
    public class RequestStockCsvHandlerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldReturnMessageWithQuantity()
        {
            var handler = new RequestStockCsvHandler();
            var context = new TestableMessageHandlerContext();

            var message = new RequestStockCSV { Stock = "aapl.us", Id = Guid.NewGuid().ToString(), ChatRoomId = "sd" };

            await handler.Handle(message, context)
                        .ConfigureAwait(false);
            var processMessage = (ReponseStockCsv)context.SentMessages[0].Message;
            var getNum = Regex.Match(processMessage.BotMessage, @"\d+\.*\d*").Value;
            var assertIsDouble = double.TryParse(getNum, out double _);
            Assert.True(assertIsDouble);
        }
    }
}
