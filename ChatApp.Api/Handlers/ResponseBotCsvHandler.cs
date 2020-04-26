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
        //private readonly IChatService  _hubContext;
        //IHubContext hub = GlobalHost.ConnectionManager.GetHubContext<ChatAppHub>();
        public ResponseBotCsvHandler(IHubContext<ChatAppHub> hubContext) 
        {
            _hubContext = hubContext;
        }
        public async Task Handle(ReponseStockCsv message, IMessageHandlerContext context)
        {
            //var _hubClient = (IHubContext<ChatAppHub>)Dependency.ServiceProvider.GetService(typeof(IHubContext<ChatAppHub>));
            
            var response = new ChatRoomMessageResponseHubDto();
                response.ChatRoomId = message.ChatRoomId;
                response.Sender = "bot";
                response.MessageDate = DateTime.Now;
                response.Message = message.BotMessage;

            await _hubContext.Clients.All
                .SendAsync(HubConstants.ON_MSG_RECVD, response);
        }
    }
}
