using AutoMapper;
using ChatApp.Model.Contexts.ChatApp;
using ChatApp.Model.UnitOfWorks;
using FluentValidation;
using ChatApp.Bl.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class ChatRoomController : BaseController<ChatRoom, ChatRoomDto>
    {
        public ChatRoomController(IMapper mapper, IUnitOfWork<IChatAppDbContext> uow, IValidatorFactory validationFactory) : base(mapper, uow, validationFactory)
        {
        }
    }
}
