using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Core.Models
{
    public class TokenConfig
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

    }
}
