using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.BackgroundServices.Core.Persistence.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
