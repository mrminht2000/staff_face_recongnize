using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Persistence.Repositories;
using StaffManagement.Core.Services.Dtos;
using StaffManagement.Core.Services.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Services.Impls
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private const int MaxRequestEvent = 1000;
        private const int MaxAggregateId = 5;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public async Task<PaginationResult<Event>> QueryEventAsync(QueryEventRequest request, CancellationToken cancellationToken = default)
        {
            if (request.AggregateIds.Count() == 0)
                throw new ArgumentException("AggregateId can't be null", "AggregateId");

            request.AggregateIds = request.AggregateIds.Count() > MaxAggregateId ? request.AggregateIds.GetRange(0, MaxAggregateId) 
                                                                                 : request.AggregateIds;

            Expression<Func<Event, bool>> filters = @event => request.AggregateIds.Contains(@event.AggregateId);

            request.Skip = request.Skip < MaxRequestEvent ? request.Skip : MaxRequestEvent;

            var result = await _eventRepository
                .GetWithPaginationAsync(new PaginationParams<Event>(filters, request.SortField, request.SortDirection, request.Skip, request.Take), cancellationToken);

            return result;
        }
    }
}
