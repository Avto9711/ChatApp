using ChatApp.Core.BaseModel.BaseEntityDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Bl.Dto
{
    public class ChatRoomDto  : BaseEntityDto
    {
        public string ChatRoomName { get; set; }
        public string ChatRoomCode { get; set; }
    }
}
