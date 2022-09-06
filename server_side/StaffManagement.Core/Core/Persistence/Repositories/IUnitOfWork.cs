using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Core.Persistence.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
