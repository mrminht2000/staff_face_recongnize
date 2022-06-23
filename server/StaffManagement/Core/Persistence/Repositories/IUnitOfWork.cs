using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Persistence.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
