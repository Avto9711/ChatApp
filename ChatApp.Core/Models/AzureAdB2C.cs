using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Core.Models
{
    public class AzureAdB2C
    {
        public string Tenant { get; set; }
        public string ClientId { get; set; }
        public string ScopeRead { get; set; }
        public string Policy { get; set; }
        public string ScopeWrite { get; set; }
        public string Issuer { get; set; }
    }
}
