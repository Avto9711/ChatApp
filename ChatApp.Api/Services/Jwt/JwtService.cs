using ChatApp.Core.Models;
using ChatApp.Model.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Api.Services
{
    public class JwtService : IJwtService
    {


        private readonly TokenConfig _tokenConfig;
        public JwtService(IOptions<TokenConfig> tokenConfig)
        {
            _tokenConfig = tokenConfig.Value;
        }
        public string GenerateToken(UserApplication user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var aKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenConfig.Secret));
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, user.UserName) };
            var credentials = new SigningCredentials(aKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_tokenConfig.Issuer, _tokenConfig.Audience, claims, expires: DateTime.UtcNow.AddDays(7), signingCredentials: credentials);
            return tokenHandler.WriteToken(token);
        }
    }
}
