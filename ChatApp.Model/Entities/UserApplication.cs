﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Model.Entities
{
    public class UserApplication: IdentityUser
    {
        public string Name { get; set; }
        public string ConnectionId { get; set; }
    }
}
