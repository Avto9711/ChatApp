using ChatApp.Api.Services.Chat;
using ChatApp.Bl.Services.ChatMessage;
using ChatApp.Bl.Services.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatApp.Api.Hubs
{
    public class ChatAppHub : Hub
    {

        private readonly IChatService _chatService;
        public ChatAppHub(IChatService chatService)
        {
            _chatService = chatService;
        }
        public async Task SendMessage(string chatRoomCode,string user, string message)
        {
            var HubMessage = new Bl.Services.Models.ChatRoomMessageHubDto { ChatRoomCode = chatRoomCode, User = user, Message = message };
            var messageHub = await _chatService.ProcessMessage(HubMessage);
            await Clients.Group(chatRoomCode).SendAsync(HubConstants.ON_MSG_RECVD, messageHub);
        }

        public async Task EnrollUserToChatRoom(string chatRoomCode)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomCode);
            await Clients.Group(chatRoomCode).SendAsync(HubConstants.ON_USR_ENRLLMENT_RECVD, $"{Context.ConnectionId} has joined the group {chatRoomCode}.");
        }
    }

 



}
