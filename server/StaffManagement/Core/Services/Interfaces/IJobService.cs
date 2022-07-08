using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Services.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Services.Interfaces
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
