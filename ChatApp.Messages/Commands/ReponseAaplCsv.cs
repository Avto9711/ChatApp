using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Messages.Commands
{
    public class ReponseAaplCsv : ICommand
    {
        public string Id { get; set; }
        public string BusMessage { get; set; }
    }
}
