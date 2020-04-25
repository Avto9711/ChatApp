using ChatApp.Core.BaseModel.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ChatApp.Model.Entities
{
    public class ChatRoomMessage : BaseEntity
    {
        public string Message { get; set; }

        public DateTime MessageTime { get; set; }

        public string MessageFromUser { get; set; }

        [ForeignKey("ChatRoom")]
        public int ChatRoomId { get; set; }

        public virtual ChatRoom ChatRoom { get; set; }

    }
}
