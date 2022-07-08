using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Persistence.Repositories;
using StaffManagement.Core.Services.Dtos;
using StaffManagement.Core.Services.Interfaces;
using static StaffManagement.Core.Common.Enum.EventEnum;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using StaffManagement.Extensions;
using StaffManagement.Core.Context;
using static StaffManagement.Core.Common.Enum.UserEnum;

namespace StaffManagement.Core.Services.Impls
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationContext _authenticationContext;
        private const int MaxVacationDay = 3;
        public EventService(IEventRepository eventRepository, IUserRepository userRepository, IAuthenticationContext authenticationContext)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authenticationContext = authenticationContext ?? throw new ArgumentNullException(nameof(authenticationContext));
        }

        public async Task CreateEventAsync(Event @event, CancellationToken cancellationToken = default)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            if (@event.EndTime != null && @event.StartTime > @event.EndTime)
            {
                throw new ArgumentNullException("Ngày kết thúc không được nhỏ hơn ngày bắt đầu");
            }

            await _eventRepository.CreateAsync(@event, cancellationToken);
        }

        public async Task CreateVacationAsync(Event @event, CancellationToken cancellationToken = default)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            if (@event.EventType == (int)EventType.Vacation && @event.StartTime < DateTime.Now.Date.AddHours(-7))
            {
                throw new ArgumentNullException("Ngày bắt đầu không được nhỏ hơn ngày hiện tại");
            }

            if(@event.EndTime != null && @event.StartTime > @event.EndTime)
            {
                throw new ArgumentNullException("Ngày kết thúc không được nhỏ hơn ngày bắt đầu");
            }

            if (_authenticationContext.UserRole >= (int)Role.Admin)
            {
                @event.IsConfirmed = true;
            }

            var result = await _eventRepository.CreateAsync(@event, cancellationToken);
        }

        public async Task<Event> QueryEventByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => id == @event.Id;

            var result = await _eventRepository.GetValueAsync(new QueryParams<Event>(filters), cancellationToken);
            
            if (result == null || result.Data.Count == 0)
            {
                throw new NullReferenceException("Event not found");
            }

            return result.Data.FirstOrDefault();
        }

        public async Task<QueryResult<Event>> QueryEventsByUserIdAsync(QueryEventRequest request, CancellationToken cancellationToken = default)
        {

            Expression<Func<Event, bool>> filters = @event => request.UserId == @event.UserId;

            var result = await _eventRepository
                .GetValueAsync(new QueryParams<Event>(filters), cancellationToken);

            return result;
        }

        public async Task<QueryResult<Event>> QueryCompanyEventsAsync(CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => @event.UserId == null;

            var result = await _eventRepository
                .GetValueAsync(new QueryParams<Event>(filters), cancellationToken);

            return result;
        }

        public async Task<QueryResult<Event>> QueryUnconfirmedEventsByUserIdAsync(QueryEventRequest request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => request.UserId == @event.UserId && @event.IsConfirmed == false;

            var result = await _eventRepository
                .GetValueAsync(new QueryParams<Event>(filters), cancellationToken);

            return result;
        }

        public async Task<UserResult> QueryUnconfirmedEventsAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetUserEventsAsync(new UserParams(), cancellationToken);

            var result = users.Users.Select(u =>
            {
                var user = u;
                user.Events = user.Events.Where(e => e.IsConfirmed == false).ToList();

                return user;
            }).Where(u => u.Events.Count > 0).ToList();

            return new UserResult(result);
        }

        public async Task<QueryResult<Event>> QueryVacationEventByUserIdAsync(QueryEventRequest request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => request.UserId == @event.UserId && @event.EventType == (int)EventType.Vacation;

            var result = await _eventRepository
                .GetValueAsync(new QueryParams<Event>(filters), cancellationToken);

            return result;
        }

        public async Task UpdateEventAsync(Event request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => request.Id == @event.Id;

            await _eventRepository.UpdateAsync(new QueryParams<Event>(filters), request, cancellationToken);
        }

        public async Task AcceptEventAsync(Event request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => request.Id == @event.Id;

            var vacations = await QueryVacationEventByUserIdAsync(new QueryEventRequest { UserId = request.UserId }, cancellationToken);

            var vacationDays = vacations.Data.CountDays();

            if (vacationDays > MaxVacationDay)
            {
                request.EventType = (int)EventType.Absent;
            } else
            {
                request.EventType = (int)EventType.Vacation;
            }

            await _eventRepository.UpdateAsync(new QueryParams<Event>(filters), request, cancellationToken);
        }

        public async Task DeleteEventAsync(QueryEventRequest request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => request.EventId == @event.Id;

            await _eventRepository.DeleteAsync(new QueryParams<Event>(filters), cancellationToken);
        }
    }
}
