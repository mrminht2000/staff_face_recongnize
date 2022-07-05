using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Services.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Services.Interfaces
{
    public interface IEventService
    {
        Task<Event> CreateVacationAsync(Event @event, CancellationToken cancellationToken = default);
        
        Task<QueryResult<Event>> QueryEventsByUserIdAsync(QueryEventRequest request, CancellationToken cancellationToken = default);

        Task<UserResult> QueryUnconfirmedEventsAsync(CancellationToken cancellationToken = default);

        Task<QueryResult<Event>> QueryUnconfirmedEventsByUserIdAsync(QueryEventRequest request, CancellationToken cancellationToken = default);
        
        Task<QueryResult<Event>> QueryVacationEventByUserIdAsync(QueryEventRequest request, CancellationToken cancellationToken= default);

        Task<QueryResult<Event>> QueryCompanyEventsAsync(CancellationToken cancellationToken = default);

        Task AcceptEventAsync(Event request, CancellationToken cancellationToken = default);

        Task DeclineEventAsync(QueryEventRequest request, CancellationToken cancellationToken = default);
    }
}
