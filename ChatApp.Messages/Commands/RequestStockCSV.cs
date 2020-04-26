using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Messages.Commands
{
    public class RequestStockCSV : ICommand
    {
        public string Id { get; set; }
        public string Stock { get; set; }
        public string ChatRoomId { get; set; }
    }
}
