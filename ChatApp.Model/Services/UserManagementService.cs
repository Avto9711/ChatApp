using ChatApp.Model.Contexts.ChatApp;
using ChatApp.Model.Entities;
using ChatApp.Model.Repositories;
using ChatApp.Model.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Model.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly UserManager<UserApplication> _userManager;
        private readonly SignInManager<UserApplication> _signInManager;
        private readonly IUnitOfWork<IChatAppDbContext> _uow;
        public UserManagementService(UserManager<UserApplication> userManager, SignInManager<UserApplication> signInManager, IUnitOfWork<IChatAppDbContext> uow)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _uow = uow;
        }

        public async Task<bool> AnyUsers()
        {
            var result = _userManager.Users.Any();
            return result;
        }

        public async Task<IdentityResult> CreateUser(UserApplication user, string password)
        {
            await _userManager.CreateAsync(user, password);
            return await _userManager.AddClaimsAsync(user, new[]
             {
                new Claim(ClaimTypes.Name, user.UserName),
             });
        }


        public Task<SignInResult> IsValidUserAsync(string username, string password)
        {
            return this._signInManager.PasswordSignInAsync(username, password, false, false);
        }

        public async Task<UserApplication> GetUserByUserName(string username)
        {
            var usrDbSet = _uow.context.GetDbSet<UserApplication>();
            return await usrDbSet.Where(x => x.UserName == username).FirstOrDefaultAsync();
        }
    }
}
