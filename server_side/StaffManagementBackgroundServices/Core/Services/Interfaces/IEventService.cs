using StaffManagement.BackgroundServices.Core.Common;
using StaffManagement.BackgroundServices.Core.Persistence.Models;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.BackgroundServices.Core.Services.Interfaces
{
    public interface IEventService
    {
        Task<QueryResult<Event>> QueryRegesterEventsByUserIdAsync(long userId, CancellationToken cancellationToken = default);
        Task DeleteRegisterEventsByUserId(long userId, CancellationToken cancellationToken = default);
    }
}
