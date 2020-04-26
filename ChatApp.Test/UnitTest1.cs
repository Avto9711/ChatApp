using ChatApp.Bus.Handlers;
using ChatApp.Messages.Commands;
using NServiceBus.Testing;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ChatApp.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var handler = new RequestStockCsvHandler();
            var context = new TestableMessageHandlerContext();

            var message = new RequestStockCSV { Stock = "aapl.us", Id = Guid.NewGuid().ToString(), ChatRoomId = "sd" };

            await handler.Handle(message, context)
                        .ConfigureAwait(false);
            var processMessage = (ReponseStockCsv)context.SentMessages[0].Message;
            Assert.AreEqual(1,1);
        }
    }
}