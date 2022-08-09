 using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using StaffManagement.Core.Services.Dtos;
using StaffManagement.Core.Services.Interfaces;
using StaffManagement.ViewModels;
using StaffManagement.Middlewares.Attributes;
using StaffManagement.Core.Persistence.Models;
using static StaffManagement.Core.Common.Enum.EventEnum;

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

        [AdminRequire]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCompanyEventAsync([FromBody] EventCreateReq req)
        {
            await _eventService.CreateEventAsync(new Event
            {
                EventName = req.EventName,
                EventType = req.EventType,
                StartTime = req.StartTime,
                EndTime = req.EndTime,
                AllDay = req.AllDay,
                Per = req.Per,
                IsConfirmed = true,
                UserId = null
            });
            return Ok();
        }

        [AdminOrOwner]
        [HttpPost]
        [Route("event")]
        public async Task<IActionResult> CreateUserEventAsync([FromBody] EventCreateReq req)
        {
            await _eventService.CreateEventAsync(new Event
            {
                EventName = req.EventName,
                EventType = req.EventType,
                StartTime = req.StartTime,
                EndTime = req.EndTime,
                AllDay = req.AllDay,
                Per = req.Per,
                IsConfirmed = true,
                UserId = req.UserId
            });
             
            return Ok();
        }

        [AdminOrOwner]
        [HttpPost]
        [Route("vacation")]
        public async Task<IActionResult> CreateVacationAsync([FromBody] EventCreateReq req)
        {
            await _eventService.CreateVacationAsync(new Event
            {
                EventName = req.EventName,
                EventType = req.EventType,
                StartTime = req.StartTime,
                EndTime = req.EndTime,
                AllDay = req.AllDay,
                Per = req.Per,
                IsConfirmed = false,
                UserId = req.UserId
            });

            return Ok();
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterEventAsync([FromBody] RegisterEventReq req)
        {
            await _eventService.RegisterEventAsync(req.UserName, req.StartTime);

            return Ok();
        }

        [AdminOrOwner]
        [HttpGet]
        [Route("event")]
        public async Task<Event> QueryEventAsync(long id)
        {
            var result = await _eventService.QueryEventByIdAsync(id);

            return result;
        }

        [HttpGet]
        [Route("user")]
        public async Task<EventQueryResp> QueryEventsByUserIdAsync(long userId)
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

        [AdminOrOwner]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateEventAsync([FromBody] Event req)
        {
            if (req == null)
            {
                return BadRequest();
            }

            await _eventService.UpdateEventAsync(req);

            return Ok();
        }

        [AdminRequire]
        [HttpPut]
        [Route("confirmed")]
        public async Task<IActionResult> AcceptEventAsync([FromBody] Event unconfirmedEvent)
        {
            if (unconfirmedEvent == null)
            {
                return BadRequest();
            }

            await _eventService.AcceptEventAsync(unconfirmedEvent);

            return Ok();
        }

        [AdminOrOwner]
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteEventAsync([FromQuery] EventQueryReq req)
        {
            await _eventService.DeleteEventAsync(new QueryEventRequest
            {
                EventId = req.EventId
            });

            return Ok();
        }
    }
}
