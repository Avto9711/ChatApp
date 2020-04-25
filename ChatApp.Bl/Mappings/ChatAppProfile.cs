using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using ChatApp.Bl.Dto;
using ChatApp.Bl.Extensions;
using ChatApp.Model.Entities;

namespace ChatApp.Bl.Mappings
{
    public class ChatAppProfile : Profile
    {
        public ChatAppProfile()
        {
            this._CreateMap_WithConventions_FromAssemblies<ChatRoom, ChatRoomDto>();
        }
    }
}
