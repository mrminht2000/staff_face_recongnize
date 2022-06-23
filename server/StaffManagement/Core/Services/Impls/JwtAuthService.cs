using StaffManagement.Core.Common;
using StaffManagement.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace StaffManagement.Core.Services.Impls
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
                new Claim(JwtRegisteredClaimNames.NameId, data.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, data.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, data.FullName)
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
