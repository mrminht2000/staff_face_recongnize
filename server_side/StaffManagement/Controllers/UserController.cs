using Microsoft.AspNetCore.Mvc;
using StaffManagement.API.Core.Persistence.Models;
using StaffManagement.API.Core.Services.Dtos;
using StaffManagement.API.Core.Services.Interfaces;
using StaffManagement.API.Middlewares.Attributes;
using StaffManagement.API.ViewModels;
using System;
using System.Threading.Tasks;

namespace StaffManagement.API.Controllers
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
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateReq req)
        {
            if (req == null)
            {
                return BadRequest();
            }

            await _userService.CreateUserAsync(new User
            {
                UserName = req.UserName,
                Password = req.Password,
                FullName = req.FullName,
                Email = req.Email,
                PhoneNumber = req.PhoneNumber,
                Role = req.Role,
                JobId = req.JobId,
                DepartmentId = req.DepartmentId,
                IsConfirmed = true
            });

            return Ok();
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

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditUserAsync([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            await _userService.UpdateUserAsync(user);

            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUserAsync([FromQuery] UserQueryReq req)
        {
            await _userService.DeleteUserAsync(new QueryUserRequest
            {
                Id = req.Id
            });

            return Ok();
        }
    }
}
