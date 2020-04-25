using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Core.Models
{
    public class NServiceBusAppSettings
    {
        public string EndpointName { get; set; }
        public string DestinationName { get; set; }
        public string NServiceBusConnectionString { get; set; }
    }
}
