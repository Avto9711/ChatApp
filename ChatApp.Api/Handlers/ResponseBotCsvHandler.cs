using ChatApp.Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.Handlers
{
    public class ResponseBotCsvHandler : IHandleMessages<ReponseStockCsv>
    {
        public Task Handle(ReponseStockCsv message, IMessageHandlerContext context)
        {

            return Task.CompletedTask;
        }
    }
}
