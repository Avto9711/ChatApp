using ChatApp.Bl.Services.Models;
using ChatApp.Model.Contexts.ChatApp;
using ChatApp.Model.Entities;
using ChatApp.Model.Repositories;
using ChatApp.Model.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Bl.Services.ChatMessage
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IRepository<ChatRoomMessage> _messageRepo;
        private readonly IRepository<ChatRoom> _chatRoomRepo;
        private readonly IUnitOfWork<IChatAppDbContext> _uow;
        public ChatMessageService(IUnitOfWork<IChatAppDbContext> uow)
        {
            _messageRepo = uow.GetRepository<ChatRoomMessage>();
            _chatRoomRepo = uow.GetRepository<ChatRoom>();
            _uow = uow;
        }
        public async Task SaveMessage(ChatRoomMessageHubDto message)
        {
            int chatRoomId = _chatRoomRepo
                .Where(x => x.ChatRoomCode == message.ChatRoomCode)
                .FirstOrDefault().Id;
            _messageRepo.Add(new ChatRoomMessage
            {
                Message = message.Message,
                MessageFromUser = message.User,
                MessageTime = DateTime.Now,
                ChatRoomId = chatRoomId
            });
            await _uow.Commit();
        }
    }
}
