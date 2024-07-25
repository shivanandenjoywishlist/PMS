using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PMS_BAL.IService.Common;
using PMS_Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PMS_BAL.Service.Common
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly byte[] _secretKey;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        }

        public async Task<string> GenerateAccessToken(ApplicationUser applicationUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(_secretKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, applicationUser.Username.ToString()),
                new Claim(ClaimTypes.Role, applicationUser.Role),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(2), 
                signingCredentials: creds
            );

            return tokenHandler.WriteToken(token);
        }
    }
}
