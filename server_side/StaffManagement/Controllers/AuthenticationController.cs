using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StaffManagement.API.Core.Services.Dtos;
using StaffManagement.API.Core.Services.Interfaces;
using StaffManagement.API.ViewModels;
using System;
using System.Threading.Tasks;

namespace StaffManagement.API.Controllers
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

        public async Task<AuthUserResp> QueryAsync([FromBody] AuthUserReq req)
        {
            var result = await _authUserService.QueryUserAsync(new AuthUserRequest
            {
                Password = req.Password,
                UserName = req.UserName,
            });

            return new AuthUserResp
            {
                Token = result,
                Expires = DateTime.Now.AddDays(long.Parse(_configuration["Jwt:ExpiryInDays"]))
            };
        }


    }
}
