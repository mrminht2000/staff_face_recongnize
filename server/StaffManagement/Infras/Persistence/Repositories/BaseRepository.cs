using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Repositories;
using StaffManagement.Extensions;
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

        public virtual async Task<PaginationResult<TModel>> GetWithPaginationAsync(PaginationParams<TModel> @params, 
            CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<TModel>().AsQueryable();

            if (@params.Filters != null)
            {
                query = query.Where(@params.Filters);

            }

            if (!String.IsNullOrEmpty(@params.SortDirection) && !String.IsNullOrEmpty(@params.SortField))
            {
                switch (@params.SortDirection)
                {
                    case "asc":
                        query = query.OrderBy(@params.SortField);
                        break;
                    case "desc":
                        query = query.OrderByDescending(@params.SortField);
                        break;
                }
            }

            var total = await query.CountAsync(cancellationToken);
            var data = await query.Skip(@params.Skip).Take(@params.Take).ToListAsync(cancellationToken);

            return new PaginationResult<TModel>(data, total);
        }
    }
}
