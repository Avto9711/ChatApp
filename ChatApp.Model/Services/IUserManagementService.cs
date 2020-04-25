using ChatApp.Model.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Model.Services
{
    public interface IUserManagementService
    {
        Task<IdentityResult> CreateUser(UserApplication user, string password);
        Task<SignInResult> IsValidUserAsync(string username, string password);
        Task<bool> AnyUsers();
        Task<UserApplication> GetUserByUserName(string username);

    }
}
