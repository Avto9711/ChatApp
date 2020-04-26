using ChatApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.Services
{
    public interface IJwtService
    {
        string GenerateToken(UserApplication user);
    }
}
