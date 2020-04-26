using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Bl.Services.Models
{
    public class ChatRoomMessageResponseHubDto
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public string ChatRoomId { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
