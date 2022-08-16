using Microsoft.AspNetCore.Mvc;
using StaffManagement.API.Core.Persistence.Models;
using StaffManagement.API.Core.Services.Interfaces;
using StaffManagement.API.Middlewares.Attributes;
using StaffManagement.API.ViewModels;
using System;
using System.Threading.Tasks;

namespace StaffManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        public JobController(IJobService jobService)
        {
            _jobService = jobService ?? throw new ArgumentNullException(nameof(jobService));
        }

        [AdminRequire]
        [HttpPost]
        public async Task<IActionResult> CreateJobAsync(Job req)
        {
            await _jobService.CreateJobAsync(req);

            return Ok();
        }

        [HttpGet]
        [Route("job")]
        public async Task<Job> GetJobAsync(long jobId)
        {
            var result = await _jobService.QueryJobAsync(jobId);

            return result;
        }

        [HttpGet]
        public async Task<JobQueryResp> GetJobsAsync()
        {
            var result = await _jobService.QueryJobsAsync();

            return new JobQueryResp
            {
                Jobs = result.Data
            };
        }

        [AdminRequire]
        [HttpPut]
        public async Task<IActionResult> UpdateJobAsync(Job req)
        {
            await _jobService.UpdateJobAsync(req);

            return Ok();
        }

        [AdminRequire]
        [HttpDelete]
        public async Task<IActionResult> DeleteJobAsync(long jobId)
        {
            await _jobService.DeleteJobAsync(jobId);

            return Ok();
        }
    }
}
