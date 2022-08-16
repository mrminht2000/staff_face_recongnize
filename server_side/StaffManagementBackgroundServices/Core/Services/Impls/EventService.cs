using StaffManagement.BackgroundServices.Core.Common;
using StaffManagement.BackgroundServices.Core.Persistence.Models;
using StaffManagement.BackgroundServices.Core.Persistence.Repositories;
using StaffManagement.BackgroundServices.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static StaffManagement.BackgroundServices.Core.Common.Enum.EventEnum;

namespace StaffManagement.BackgroundServices.Core.Services.Impls
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<QueryResult<Event>> QueryRegesterEventsByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        {
            var dateNow = DateTime.UtcNow.Date;
            Expression<Func<Event, bool>> filters = @event => userId == @event.UserId && @event.EventType == (int)EventType.Register && @event.StartTime.Date == dateNow;

            var result = await _eventRepository
                .GetAsync(new QueryParams<Event>(filters), cancellationToken);

            return result;
        }

        public async Task DeleteRegisterEventsByUserId(long userId, CancellationToken cancellationToken = default)
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
