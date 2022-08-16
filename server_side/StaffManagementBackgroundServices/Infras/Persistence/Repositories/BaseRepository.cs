using Microsoft.EntityFrameworkCore;
using StaffManagement.BackgroundServices.Core.Common;
using StaffManagement.BackgroundServices.Core.Persistence.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.BackgroundServices.Infras.Persistence.Repositories
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
        }

        public virtual async Task<QueryResult<TModel>> GetAsync(QueryParams<TModel> @params,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<TModel>().AsQueryable();

            if (@params.Filters != null)
            {
                query = query.Where(@params.Filters).AsNoTracking();

            }

            var data = await query.ToListAsync(cancellationToken);

            return new QueryResult<TModel>(data);
        }

        public virtual void Update(QueryParams<TModel> @params, TModel obj)
        {
            var query = _dbContext.Set<TModel>().AsQueryable();

            if (@params.Filters != null)
            {
                query = query.Where(@params.Filters).AsNoTracking();

            }
            else
            {
                throw new NullReferenceException("Invalid request");
            }

            if (query.Count() <= 0)
            {
                throw new NullReferenceException("Record not found");
            }

            _dbContext.Update(obj);
        }

        public virtual void Delete(QueryParams<TModel> @params)
        {
            var query = _dbContext.Set<TModel>().AsQueryable();

            if (@params.Filters != null)
            {
                query = query.Where(@params.Filters).AsNoTracking();

            }
            else
            {
                throw new NullReferenceException("Invalid request");
            }

            if (query.Count() <= 0)
            {
                throw new NullReferenceException("Record not found");
            }

            _dbContext.RemoveRange(query);
        }
    }
}
