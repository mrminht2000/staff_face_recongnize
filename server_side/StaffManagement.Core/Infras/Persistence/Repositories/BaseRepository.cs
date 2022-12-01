using Microsoft.EntityFrameworkCore;
using StaffManagement.Core.Core.Common;
using StaffManagement.Core.Core.Persistence.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Infras.Persistence.Repositories
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
    {
        protected readonly MsSqlStaffManagementDbContext _dbContext;

        protected BaseRepository(MsSqlStaffManagementDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual void Create(TModel obj)
        {
            _dbContext.Set<TModel>().Add(obj);

            ;
        }

        public virtual async Task<QueryResult<TModel>> GetAsync(QueryParams<TModel> @params,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<TModel>().AsQueryable();

            if (@params.Filters != null)
            {
                query = query.Where(@params.Filters);

            }

            var data = await query.ToListAsync(cancellationToken);

            return new QueryResult<TModel>(data);
        }

        public virtual void Update(QueryParams<TModel> @params, TModel obj)
        {
            var query = _dbContext.Set<TModel>().AsQueryable();

            if (@params.Filters != null)
            {
                query = query.Where(@params.Filters);

            }
            else
            {
                throw new NullReferenceException("Invalid request");
            }

            if (query.Count() <= 0)
            {
                throw new NullReferenceException("Record not found");
            }

            var item = query.FirstOrDefault();

            item = obj;

            _dbContext.Update(item);
        }

        public virtual void Delete(QueryParams<TModel> @params)
        {
            var query = _dbContext.Set<TModel>().AsQueryable();

            if (@params.Filters != null)
            {
                query = query.Where(@params.Filters);

            }
            else
            {
                throw new NullReferenceException("Invalid request");
            }

            if (query.Count() <= 0)
            {
                throw new NullReferenceException("Record not found");
            }
            
            foreach (var item in query)
            {
                _dbContext.Remove(item);
            }
        }
    }
}
