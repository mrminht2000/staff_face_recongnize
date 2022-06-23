using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Services.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Services.Interfaces
{
    public interface IEventService
    {
        Task<PaginationResult<Event>> QueryEventAsync(QueryEventRequest request, CancellationToken cancellationToken = default);
    }
}
