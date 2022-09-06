using StaffManagement.Core.Core.Common;
using StaffManagement.Core.Core.Context;
using StaffManagement.Core.Core.Persistence.Models;
using StaffManagement.Core.Core.Persistence.Repositories;
using StaffManagement.Core.Core.Services.Dtos;
using StaffManagement.Core.Core.Services.Interfaces;
using StaffManagement.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static StaffManagement.Core.Core.Common.Enum.EventEnum;
using static StaffManagement.Core.Core.Common.Enum.UserEnum;

namespace StaffManagement.Core.Core.Services.Impls
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWorkingProgressService _workingProgressService;
        private readonly IAuthenticationContext _authenticationContext;
        private readonly IUnitOfWork _unitOfWork;
        private const int MaxVacationDay = 3;

        public EventService(IEventRepository eventRepository, IUserRepository userRepository, IAuthenticationContext authenticationContext, IUnitOfWork unitOfWork, IWorkingProgressService workingProgressService)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authenticationContext = authenticationContext ?? throw new ArgumentNullException(nameof(authenticationContext));
            _workingProgressService = workingProgressService ?? throw new ArgumentNullException(nameof(workingProgressService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
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

            _eventRepository.Create(@event);

            await _unitOfWork.CommitAsync(cancellationToken);
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

            if (@event.EndTime != null && @event.StartTime > @event.EndTime)
            {
                throw new ArgumentNullException("Ngày kết thúc không được nhỏ hơn ngày bắt đầu");
            }

            if (_authenticationContext.UserRole >= (int)Role.Admin)
            {
                @event.IsConfirmed = true;
            }

            _eventRepository.Create(@event);

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task RegisterEventAsync(string userName, DateTime startTime, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new NullReferenceException("UserName is null");
            }

            Expression<Func<User, bool>> filters = @user => @user.UserName.ToLower().Equals(userName.ToLower());

            var users = await _userRepository.GetUserAsync(new UserParams(filters), cancellationToken);

            if (users.Users.Count == 0)
            {
                throw new NullReferenceException("User Not Found");
            }

            var @event = new Event
            {
                EventName = "Điểm danh",
                EventType = (int)EventType.Register,
                AllDay = false,
                StartTime = startTime,
                EndTime = null,
                IsConfirmed = true,
                Per = null,
                UserId = users.Users.FirstOrDefault().Id
            };

            _eventRepository.Create(@event);

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<Event> QueryEventByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => id == @event.Id;

            var result = await _eventRepository.GetAsync(new QueryParams<Event>(filters), cancellationToken);

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
                .GetAsync(new QueryParams<Event>(filters), cancellationToken);

            return result;
        }

        public async Task<QueryResult<Event>> QueryRegesterEventsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        {

            Expression<Func<Event, bool>> filters = @event => userId == @event.UserId && @event.EventType == (int)EventType.Register;

            var result = await _eventRepository
                .GetAsync(new QueryParams<Event>(filters), cancellationToken);

            return result;
        }

        public async Task<QueryResult<Event>> QueryCompanyEventsAsync(CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => @event.UserId == null;

            var result = await _eventRepository
                .GetAsync(new QueryParams<Event>(filters), cancellationToken);

            return result;
        }

        public async Task<QueryResult<Event>> QueryUnconfirmedEventsByUserIdAsync(QueryEventRequest request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => request.UserId == @event.UserId && @event.IsConfirmed == false;

            var result = await _eventRepository
                .GetAsync(new QueryParams<Event>(filters), cancellationToken);

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

        public async Task<QueryResult<Event>> QueryMonthEventsByUserId(long userId, CancellationToken cancellationToken = default)
        {
            var date = DateTime.UtcNow;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddYears(-1);

            Expression<Func<Event, bool>> filters = @event => userId == @event.UserId 
                                                              && @event.StartTime >= firstDayOfMonth 
                                                              && @event.StartTime <= lastDayOfMonth;

            var result = await _eventRepository
                .GetAsync(new QueryParams<Event>(filters), cancellationToken);

            return result;
        }

        public async Task<QueryResult<Event>> QueryVacationEventByUserIdAsync(QueryEventRequest request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => request.UserId == @event.UserId && @event.EventType == (int)EventType.Vacation;

            var result = await _eventRepository
                .GetAsync(new QueryParams<Event>(filters), cancellationToken);

            return result;
        }

        public async Task UpdateEventAsync(Event request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => request.Id == @event.Id;

            _eventRepository.Update(new QueryParams<Event>(filters), request);

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task AcceptEventAsync(Event request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => request.Id == @event.Id;

            var vacations = await QueryVacationEventByUserIdAsync(new QueryEventRequest { UserId = request.UserId }, cancellationToken);

            var vacationDays = vacations.Data.CountDays();

            if (vacationDays > MaxVacationDay)
            {
                request.EventType = (int)EventType.Absent;
            }
            else
            {
                request.EventType = (int)EventType.Vacation;
            }

            _eventRepository.Update(new QueryParams<Event>(filters), request);

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task DeleteEventAsync(QueryEventRequest request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Event, bool>> filters = @event => request.EventId == @event.Id;

            _eventRepository.Delete(new QueryParams<Event>(filters));

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task ProccessRegisterEventsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        {
            var registerEvents = await QueryRegesterEventsByUserIdAsync(userId, cancellationToken);

            if (registerEvents.Data.Count > 2)
            {
                var deleteEvents = new List<long>();
                for (int i = 1; i < registerEvents.Data.Count - 1; i++)
                {
                    deleteEvents.Add(registerEvents.Data[i].Id);
                }

                Expression<Func<Event, bool>> filters = @event => deleteEvents.Contains(@event.Id);

                _eventRepository.Delete(new QueryParams<Event>(filters));
            }

            if (registerEvents.Data.Count > 0)
            {
                var checkInEvent = registerEvents.Data[0];

                checkInEvent.EventName = "Check-in";
                Expression<Func<Event, bool>> checkInFilters = @event => checkInEvent.Id == @event.Id;
                _eventRepository.Update(new QueryParams<Event>(checkInFilters), checkInEvent);

                if (registerEvents.Data.Count > 1)
                {
                    var checkOutEvent = registerEvents.Data[registerEvents.Data.Count - 1];

                    checkOutEvent.EventName = "Check-out";

                    Expression<Func<Event, bool>> checkOutFilters = @event => checkOutEvent.Id == @event.Id;
                    _eventRepository.Update(new QueryParams<Event>(checkOutFilters), checkOutEvent);
                }
            }

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
