using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StaffManagement.API.Core.Services.Dtos;
using StaffManagement.API.Core.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace StaffManagement.API.Core.Services.Impls
{
    public class JwtAuthService : IAuthTokenService
    {
        private readonly IConfiguration _config;
        private readonly string _key;
        private readonly string _issuer;
        private readonly long _expiry;
        public JwtAuthService(IConfiguration config)
        {
            _config = config;
            _key = _config["Jwt:AuthKey"];
            _issuer = _config["Jwt:Issuer"];
            _expiry = long.Parse(_config["Jwt:ExpiryInDays"]);
        }
        public string GenerateToken(UserData data)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()),
                new Claim("user_id", data.Id.ToString()),
                new Claim("user_name", data.UserName),
                new Claim("full_name", data.FullName),
                new Claim("role", data.Role.ToString())
            };

            var token = new JwtSecurityToken(
              issuer: _issuer,
              claims: claims,
              expires: DateTime.Now.AddDays(_expiry),
              signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool VerifyToken(string token)
        {
            if (token == null)
            {
                return false;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidIssuer = _issuer,
                    IssuerSigningKey = securityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var claimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return claimValue;
        }
    }
}
