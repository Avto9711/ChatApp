using ChatApp.Core.BaseModel.BaseDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Bl.Dto
{
    public class ChatRoomMessageDto: BaseDto
    {
        public string Message { get; set; }

        public DateTime MessageTime { get; set; }

        public string MessageFromUser { get; set; }

        public int ChatRoomId { get; set; }

        public string ChatRoomName { get; set; }
    }
}
