using ChatApp.Core.BaseModel.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Model.Entities
{
    public class ChatRoom : BaseEntity
    {

        public ChatRoom()
        {
            this.Messages = new HashSet<ChatRoomMessage>();
        }
        public string ChatRoomName { get; set; }
        public string ChatRoomCode { get; set; }

        public ICollection<ChatRoomMessage> Messages { get; set; }
    }
}
