using ChatApp.Api.Hubs;
using ChatApp.Api.Services.Chat;
using ChatApp.Bl.Services.Models;
using ChatApp.Core.IoC;
using ChatApp.Messages.Commands;
using Microsoft.AspNetCore.SignalR;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.Handlers
{
    public class ResponseBotCsvHandler : IHandleMessages<ReponseStockCsv>
    {
        private readonly IHubContext<ChatAppHub> _hubContext;

        public ResponseBotCsvHandler(IHubContext<ChatAppHub> hubContext) 
        {
            _hubContext = hubContext;
        }
        public async Task Handle(ReponseStockCsv message, IMessageHandlerContext context)
        {
            
            var response = new ChatRoomMessageResponseHubDto();
                response.ChatRoomId = message.ChatRoomId;
                response.Sender = "bot";
                response.MessageDate = DateTime.Now;
                response.Message = message.BotMessage;

            await _hubContext.Clients.Group(message.ChatRoomId)
                .SendAsync(HubConstants.ON_MSG_RECVD, response);
        }
    }
}
