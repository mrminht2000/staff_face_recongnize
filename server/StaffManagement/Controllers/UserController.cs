using Microsoft.AspNetCore.Mvc;
using StaffManagement.Core.Services.Dtos;
using StaffManagement.Core.Services.Interfaces;
using StaffManagement.Middlewares.Attributes;
using StaffManagement.ViewModels;
using System;
using System.Threading.Tasks;

namespace StaffManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [AdminRequire]
        [HttpGet]
        public async Task<UserQueryResp> QueryUsersAsync()
        {
            var result = await _userService.QueryUsersAsync();

            return new UserQueryResp
            {
                Users = result.Users
            };
        }

        [AdminRequire]
        [HttpGet]
        [Route("events")]
        public async Task<UserQueryResp> QueryUsersEventsAsync()
        {
            var result = await _userService.QueryUsersEventsAsync();

            return new UserQueryResp
            {
                Users = result.Users
            };
        }

        [HttpGet]
        [Route("info")]
        public async Task<UserData> QueryUserByIdAsync([FromQuery] UserQueryReq req)
        {
            var result = await _userService.QueryUserByIdAsync(new QueryUserRequest { Id = req.Id });

            return result;
        }
    }
}
