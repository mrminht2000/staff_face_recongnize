using StaffManagement.Core.Core.Persistence.Models;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Core.Services.Interfaces
{
    public interface IWorkingProgressService
    {
        Task<WorkingProgress> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<WorkingProgress> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default);

        Task CreateOrUpdateAsync(WorkingProgress workingProgress, CancellationToken cancellationToken = default);
    }
}
