using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Messages.Commands
{
    public class ReponseStockCsv : ICommand
    {
        public string Id { get; set; }
        public string BotMessage { get; set; }
        public string ChatRoomId { get; set; }

    }
}
