using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Middlewares
{
    public class AuthenticationMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public AuthenticationMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.GetEndpoint()?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            attackAccountToContext(context, token);

            await _next(context);
        }

        private void attackAccountToContext(HttpContext context, string token)
        {
            if (token != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:AuthKey"]));
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
                        ValidIssuer = _config["Jwt:Issuer"],
                        IssuerSigningKey = securityKey,
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;

                    context.Items["Account"] = jwtToken.Claims.First(claim => claim.Type == "user_id").Value;
                    context.Items["AccountRole"] = jwtToken.Claims.First(claim => claim.Type == "role").Value;

                }
                catch   
                {
                    throw new UnauthorizedAccessException("Authorize token is invalid");
                }
            } else
            {
                throw new UnauthorizedAccessException("Missing authorize token");
            }
        }
    }
}
