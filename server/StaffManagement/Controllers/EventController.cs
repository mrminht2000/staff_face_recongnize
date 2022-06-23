using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using StaffManagement.Core.Services.Dtos;
using StaffManagement.Core.Services.Interfaces;
using StaffManagement.ViewModels;
using StaffManagement.Middlewares.Attributes;

namespace StaffManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        [HttpPost]
        public async Task<EventQueryResp> QueryAsync([FromBody] EventQueryReq req)
        {
            var result = await _eventService.QueryEventAsync(new QueryEventRequest
            {
                BranchId = req.BranchId,
                AggregateIds = req.AggregateId.ConvertAll(Guid.Parse),
                SortField = req.SortField,
                SortDirection = req.SortDirection,
                Skip = req.Skip,
                Take = req.Take
            });

            return new EventQueryResp
            {
                Data = result.Data,
                Total = result.Total
            };
        }
    }
}
