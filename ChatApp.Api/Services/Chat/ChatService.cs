using ChatApp.Bl.Services.ChatMessage;
using ChatApp.Bl.Services.Models;
using ChatApp.Messages.Commands;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.Services.Chat
{
    public class ChatService: IChatService
    {
        private readonly IMessageSession _bus;
        private readonly IChatMessageService _chatMessageService;


        public ChatService(IMessageSession bus, IChatMessageService chatMessageService)
        {
            _bus = bus;
            _chatMessageService = chatMessageService;

        }

        public async Task<ChatRoomMessageResponseHubDto> ProcessMessage(ChatRoomMessageHubDto hubMessage)
        {
            var response = new ChatRoomMessageResponseHubDto();

            if (isBotCommand(hubMessage.Message))
            {
                var command = hubMessage.Message.Split("=");
                switch (command[0])
                {
                    case BotCommands.StockCommand:
                        var param = command[1];
                        try
                        {
                            await _bus.Send(new RequestStockCSV { Id = Guid.NewGuid().ToString(), Stock = param, ChatRoomId = hubMessage.ChatRoomCode });

                        }
                        catch (Exception ex)
                        {
                            response.ChatRoomId = hubMessage.ChatRoomCode;
                            response.Sender = "bot";
                            response.Message = "Ups, looks like my service is not running. Try again later :c";
                            response.MessageDate = DateTime.Now;
                            break;
                        }
                        response.ChatRoomId = hubMessage.ChatRoomCode;
                        response.Sender = "bot";
                        response.Message = "Hello, In a moment you will receive the requested stock information.";
                        response.MessageDate = DateTime.Now;
                        break;

                    default:
                        Console.WriteLine("Handle unknow commands here");
                        response.ChatRoomId = hubMessage.ChatRoomCode;
                        response.Sender = "bot";
                        response.Message = "Sorry I could not understand the command. :c";
                        response.MessageDate = DateTime.Now;
                        break;
                }
            }
            else
            {
                await _chatMessageService.SaveMessage(hubMessage).ConfigureAwait(false);
                response.ChatRoomId = hubMessage.ChatRoomCode;
                response.Sender = hubMessage.User;
                response.Message = hubMessage.Message;
                response.MessageDate = DateTime.Now;

            }

            return response;
        }

        private bool isBotCommand(string message)
        {
            return message.Contains("/");
        }
    }

    public static class BotCommands
    {
        public const string StockCommand = "/stock";
    }
}
