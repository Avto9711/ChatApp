using ChatApp.Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.Handlers
{
    public class ResponseAaplCsvHandler : IHandleMessages<ReponseAaplCsv>
    {
        public Task Handle(ReponseAaplCsv message, IMessageHandlerContext context)
        {

            return Task.CompletedTask;
        }
    }
}
