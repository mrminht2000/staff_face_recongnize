using StaffManagement.Core.Services.Dtos;
using StaffManagement.Core.Services.Interfaces;
using StaffManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace StaffManagement.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthUserService _authUserService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IAuthUserService authUserService, IConfiguration configuration)
        {
            _authUserService = authUserService ?? throw new ArgumentNullException(nameof(authUserService));
            _configuration = configuration;
        }

        [HttpPost]
        
        public async Task<AuthUserResp> QueryAsync ([FromBody]AuthUserReq req)
        {
            var result = await _authUserService.QueryUserAsync(new AuthUserRequest
            {
                Password = req.Password,
                UserName = req.UserName,
            });

            return new AuthUserResp { 
                Token = result,
                Expires = DateTime.Now.AddDays(long.Parse(_configuration["Jwt:ExpiryInDays"]))
            };  
        }


    }
}
