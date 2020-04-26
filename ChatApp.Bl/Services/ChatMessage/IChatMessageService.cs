using ChatApp.Bl.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Bl.Services.ChatMessage
{
    public interface IChatMessageService
    {
        Task SaveMessage(ChatRoomMessageHubDto message);
    }
}
