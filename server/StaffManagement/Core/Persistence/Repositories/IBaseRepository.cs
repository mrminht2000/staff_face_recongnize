using StaffManagement.Core.Common;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Persistence.Repositories
{
    public interface IBaseRepository<TModel>
    {
        Task<PaginationResult<TModel>> GetWithPaginationAsync(PaginationParams<TModel> @params, CancellationToken cancellationToken);
    }
}
