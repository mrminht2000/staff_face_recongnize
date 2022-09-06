using Microsoft.AspNetCore.Mvc;
using StaffManagement.Core.Core.Persistence.Models;
using StaffManagement.Core.Core.Services.Interfaces;
using StaffManagement.API.Middlewares.Attributes;
using StaffManagement.Core.ViewModels;
using System;
using System.Threading.Tasks;

namespace StaffManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService ?? throw new ArgumentNullException(nameof(departmentService));
        }

        [AdminRequire]
        [HttpPost]
        public async Task<IActionResult> CreateDepartmentAsync(Department req)
        {
            await _departmentService.CreateDepartmentAsync(req);

            return Ok();
        }

        [HttpGet]
        [Route("department")]
        public async Task<Department> GetDepartmentAsync(long departmentId)
        {
            var result = await _departmentService.QueryDepartmentAsync(departmentId);

            return result;
        }

        [HttpGet]
        public async Task<DepartmentQueryResp> GetDepartmentsAsync()
        {
            var result = await _departmentService.QueryDepartmentsAsync();

            return new DepartmentQueryResp
            {
                Departments = result.Data
            };
        }

        [AdminRequire]
        [HttpPut]
        public async Task<IActionResult> UpdateDepartmentAsync(Department req)
        {
            await _departmentService.UpdateDepartmentAsync(req);

            return Ok();
        }

        [AdminRequire]
        [HttpDelete]
        public async Task<IActionResult> DeleteDepartmentAsync(long departmentId)
        {
            await _departmentService.DeleteDepartmentAsync(departmentId);

            return Ok();
        }
    }
}
