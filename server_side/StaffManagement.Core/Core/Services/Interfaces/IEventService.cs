using StaffManagement.Core.Core.Common;
using StaffManagement.Core.Core.Persistence.Models;
using StaffManagement.Core.Core.Services.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Core.Services.Interfaces
{
    public interface IEventService
    {
        Task CreateEventAsync(Event @event, CancellationToken cancellationToken = default);

        Task CreateVacationAsync(Event @event, CancellationToken cancellationToken = default);

        Task RegisterEventAsync(string userName, DateTime startTime, CancellationToken cancellationToken = default);

        Task<Event> QueryEventByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<QueryResult<Event>> QueryEventsByUserIdAsync(QueryEventRequest request, CancellationToken cancellationToken = default);

        Task<QueryResult<Event>> QueryEventsByUserIdAsync(QueryEventRequest request, DateTime date, CancellationToken cancellationToken = default);

        Task<QueryResult<Event>> QueryRegisterEventsByUserIdAsync(long userId, CancellationToken cancellationToken = default);

        Task<QueryResult<Event>> QueryRegisterEventsByUserIdAsync(long userId, DateTime date, CancellationToken cancellationToken = default);

        Task<UserResult> QueryUnconfirmedEventsAsync(CancellationToken cancellationToken = default);

        Task<QueryResult<Event>> QueryUnconfirmedEventsByUserIdAsync(QueryEventRequest request, CancellationToken cancellationToken = default);

        Task<QueryResult<Event>> QueryMonthEventsByUserId(long userId, CancellationToken cancellationToken = default);

        Task<QueryResult<Event>> QueryVacationEventByUserIdAsync(QueryEventRequest request, CancellationToken cancellationToken = default);

        Task<QueryResult<Event>> QueryCompanyEventsAsync(CancellationToken cancellationToken = default);

        Task UpdateEventAsync(Event request, CancellationToken cancellationToken = default);

        Task AcceptEventAsync(Event request, CancellationToken cancellationToken = default);

        Task DeleteEventAsync(QueryEventRequest request, CancellationToken cancellationToken = default);

        Task ProccessRegisterEventsByUserIdAsync(long userId, CancellationToken cancellationToken = default);
    }
}
