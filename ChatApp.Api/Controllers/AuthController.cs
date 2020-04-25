using ChatApp.Api.Services;
using ChatApp.Core.BaseModel.BaseDto;
using ChatApp.Model.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly IJwtService _service;
        private readonly IUserManagementService _userManagementService;
        public AuthController(IJwtService service, IUserManagementService userManagementService)
        {
            _service = service;
            _userManagementService = userManagementService;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginInfo)
        {
            var loginResult =  await _userManagementService.IsValidUserAsync(loginInfo.UserName, loginInfo.Password);
            if (loginResult.Succeeded)
            {
                var user =  await _userManagementService.GetUserByUserName(loginInfo.UserName);
                var token = _service.GenerateToken(user);
                return Ok(new { user ,token});
            }
            else
            {
                return BadRequest(new { Error= "Error logging" });
            }
        }
    }

    public class LoginDto: BaseDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
