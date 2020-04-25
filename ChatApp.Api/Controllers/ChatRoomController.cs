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
using ChatApp.Model.Repositories;

namespace ChatApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class ChatRoomController : BaseController<ChatRoom, ChatRoomDto>
    {
        private readonly IRepository<ChatRoomMessage> _messagesRepo;
        public ChatRoomController(IMapper mapper, 
            IUnitOfWork<IChatAppDbContext> uow, 
            IValidatorFactory validationFactory) : base(mapper, uow, validationFactory)
        {
            _messagesRepo = _uow.GetRepository<ChatRoomMessage>();
        }

        /// <summary>
        /// Returns specific Chatroom Messages.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific record.</returns>
        [HttpGet("{id}/Messages")]
        public IActionResult Messages(int id)
        {
            var messages = _messagesRepo.WhereAsNoTracking(x => x.ChatRoomId == id).OrderBy(x => x.MessageTime);
            var dto = _mapper.Map<List<ChatRoomMessageDto>>(messages);
            return Ok(dto);
        }


    }
}
