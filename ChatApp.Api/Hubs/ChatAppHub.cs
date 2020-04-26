using ChatApp.Bl.Services.ChatMessage;
using ChatApp.Bl.Services.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatApp.Api.Hubs
{
    public class ChatAppHub : Hub
    {
        private readonly IChatMessageService _chatMessageService;
        public ChatAppHub(IChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }
        public async Task SendMessage(string chatRoomCode,string user, string message)
        {
            var HubMessage = new Bl.Services.Models.ChatRoomMessageHubDto { ChatRoomCode = chatRoomCode, User = user, Message = message };
            await _chatMessageService.SaveMessage(HubMessage).ConfigureAwait(false);

            var messageHub = new ChatRoomMessageResponseHubDto { ChatRoomId = chatRoomCode, Sender = user, Message = message, MessageDate = DateTime.Now };
            await Clients.Group(chatRoomCode).SendAsync(HubConstants.ON_MSG_RECVD, messageHub);
        }

        public async Task EnrollUserToChatRoom(string chatRoomCode,string connectionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomCode);
            await Clients.Group(chatRoomCode).SendAsync(HubConstants.ON_USR_ENRLLMENT_RECVD, $"{Context.ConnectionId} has joined the group {chatRoomCode}.");
        }
    }

    public static class HubConstants
    {
        public static string ON_USR_ENRLLMENT_RECVD = "OnUserEnrollmentMessage";
        public static string ON_MSG_RECVD = "OnChatRoomMessage";
    }



}
