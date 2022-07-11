using Microsoft.AspNetCore.Mvc;
using StaffManagement.Core.Common;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Persistence.Repositories
{
    public interface IBaseRepository<TModel>
    {
        void Create(TModel obj);
        Task<QueryResult<TModel>> GetAsync(QueryParams<TModel> @params, CancellationToken cancellationToken);
        void Update(QueryParams<TModel> @params, TModel obj);
        void Delete(QueryParams<TModel> @params);
    }
}
