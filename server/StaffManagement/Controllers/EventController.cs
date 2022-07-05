 using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using StaffManagement.Core.Services.Dtos;
using StaffManagement.Core.Services.Interfaces;
using StaffManagement.ViewModels;
using StaffManagement.Middlewares.Attributes;
using StaffManagement.Core.Persistence.Models;

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

        [AdminOrOwner]
        [HttpPost]
        [Route("vacation")]
        public async Task<IActionResult> CreateVacationAsync([FromBody] EventCreateReq req)
        {
            var result = await _eventService.CreateVacationAsync(new Event
            {
                EventName = req.EventName,
                EventType = req.EventType,
                StartTime = req.StartTime,
                EndTime = req.EndTime,
                AllDay = req.AllDay,
                Per = req.Per,
                IsConfirmed = req.IsConfirmed,
                UserId = req.UserId
            });

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("info")]
        public async Task<EventQueryResp> QueryAsync(long userId)
        {
            var result = await _eventService.QueryEventsByUserIdAsync(new QueryEventRequest
            {
                UserId = userId
            });

            return new EventQueryResp
            {
                Events = result.Data
            };
        }

        [AdminOrOwner]
        [HttpGet]
        [Route("unconfirmed/user")]
        public async Task<EventQueryResp> QueryUnconfirmedByUserIdAsync(long userId)
        {
            var result = await _eventService.QueryUnconfirmedEventsByUserIdAsync(new QueryEventRequest
            {
                UserId = userId
            });

            return new EventQueryResp
            {
                Events = result.Data
            };
        }

        [AdminRequire]
        [HttpGet]
        [Route("unconfirmed")]
        public async Task<UserQueryResp> QueryUnconfirmedAsync()
        {
            var result = await _eventService.QueryUnconfirmedEventsAsync();

            return new UserQueryResp
            {
                Users = result.Users
            };
        }

        [HttpGet]
        public async Task<EventQueryResp> GetCompanyEventsAsync()
        {
            var result = await _eventService.QueryCompanyEventsAsync();

            return new EventQueryResp
            {
                Events = result.Data
            };
        }

        [AdminRequire]
        [HttpPut]
        [Route("confirmed")]
        public async Task<IActionResult> AcceptEventAsync([FromBody] Event unconfirmedEvent)
        {
            await _eventService.AcceptEventAsync(unconfirmedEvent);

            return Ok();
        }

        [AdminOrOwner]
        [HttpDelete]
        [Route("decline")]
        public async Task<IActionResult> DeclineEventAsync([FromQuery] EventQueryReq req)
        {
            await _eventService.DeclineEventAsync(new QueryEventRequest
            {
                EventId = req.EventId
            });

            return Ok();
        }
    }
}
