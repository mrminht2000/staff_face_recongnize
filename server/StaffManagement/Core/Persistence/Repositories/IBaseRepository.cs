using Microsoft.AspNetCore.Mvc;
using StaffManagement.Core.Common;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Persistence.Repositories
{
    public interface IBaseRepository<TModel>
    {
        Task<TModel> CreateAsync(TModel obj, CancellationToken cancellationToken);
        Task<QueryResult<TModel>> GetValueAsync(QueryParams<TModel> @params, CancellationToken cancellationToken);
        Task UpdateAsync(QueryParams<TModel> @params, TModel obj, CancellationToken cancellationToken);
        Task DeleteAsync(QueryParams<TModel> @params, CancellationToken cancellationToken);
    }
}
