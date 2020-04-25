using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Bl.Services.Models
{
    public class ChatRoomMessageHubDto
    {
        public string User { get; set; }
        public string Message { get; set; }
        public string ChatRoomCode { get; set; }
    }
}
