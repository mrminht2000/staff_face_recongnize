using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Infras.Persistence.Repositories
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
    {
        private readonly MsSqlStaffManagementDbContext _dbContext;

        protected BaseRepository(MsSqlStaffManagementDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public virtual async Task<TModel> CreateAsync(TModel obj, CancellationToken cancellationToken)
        {
            var model = await _dbContext.Set<TModel>().AddAsync(obj, cancellationToken);

            await _dbContext.CommitAsync(cancellationToken);

            return model.Entity;
;       }

        public virtual async Task<QueryResult<TModel>> GetValueAsync(QueryParams<TModel> @params, 
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

        public virtual async Task UpdateAsync(QueryParams<TModel> @params, TModel obj, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<TModel>().AsQueryable();

            if (@params.Filters != null)
            {
                query = query.Where(@params.Filters);

            } else
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

            await _dbContext.CommitAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(QueryParams<TModel> @params, CancellationToken cancellationToken)
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

            _dbContext.Remove(item);

            await _dbContext.CommitAsync(cancellationToken);
        }
    }
}
