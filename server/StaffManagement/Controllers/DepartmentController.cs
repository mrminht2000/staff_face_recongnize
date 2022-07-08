using Microsoft.AspNetCore.Mvc;
using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Services.Interfaces;
using StaffManagement.Middlewares.Attributes;
using StaffManagement.ViewModels;
using System;
using System.Threading.Tasks;

namespace StaffManagement.Controllers
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
        [Route("create")]
        public async Task<IActionResult> CreateDepartmentAsync(Department req)
        {
            await _departmentService.CreateDepartmentAsync(req);

            return Ok();
        }

        [HttpGet]
        [Route("Department")]
        public async Task<Department> GetDepartmentAsync(long DepartmentId)
        {
            var result = await _departmentService.QueryDepartmentAsync(DepartmentId);

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
        [Route("update")]
        public async Task<IActionResult> UpdateDepartmentAsync(Department req)
        {
            await _departmentService.UpdateDepartmentAsync(req);

            return Ok();
        }

        [AdminRequire]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteDepartmentAsync(long DepartmentId)
        {
            await _departmentService.DeleteDepartmentAsync(DepartmentId);

            return Ok(0);
        }
    }
}
