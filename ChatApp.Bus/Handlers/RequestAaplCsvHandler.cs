using ChatApp.Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Bus.Handlers
{
    public class RequestAaplCsvHandler : IHandleMessages<RequestAaplCsv>
    {
        static ILog log = LogManager.GetLogger<RequestAaplCsvHandler>();

        public async Task Handle(RequestAaplCsv message, IMessageHandlerContext context)
        {

            await context.Send(new ReponseAaplCsv { Id = Guid.NewGuid().ToString(), BusMessage = "Mi loco, ya" });
        }
    }
}
