using ChatApp.Bl.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.Services.Chat
{
    public interface IChatService
    {
        Task<ChatRoomMessageResponseHubDto> ProcessMessage(ChatRoomMessageHubDto message);

    }
}
