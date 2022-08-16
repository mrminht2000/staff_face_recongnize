using StaffManagement.API.Core.Common;
using StaffManagement.API.Core.Persistence.Models;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.API.Core.Services.Interfaces
{
    public interface IJobService
    {
        Task CreateJobAsync(Job request, CancellationToken cancellationToken = default);

        Task<Job> QueryJobAsync(long id, CancellationToken cancellationToken = default);

        Task<QueryResult<Job>> QueryJobsAsync(CancellationToken cancellationToken = default);

        Task UpdateJobAsync(Job request, CancellationToken cancellationToken = default);

        Task DeleteJobAsync(long id, CancellationToken cancellationToken = default);
    }
}
