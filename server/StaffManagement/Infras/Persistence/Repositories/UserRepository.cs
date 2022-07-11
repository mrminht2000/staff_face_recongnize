using Microsoft.EntityFrameworkCore;
using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Persistence.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Infras.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MsSqlStaffManagementDbContext _dbContext;
        public UserRepository(MsSqlStaffManagementDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void CreateUser(User user)
        {
            _dbContext.Set<User>().Add(user);
        }

        public async Task<UserResult> GetUserAsync(UserParams @params, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<User>().AsQueryable();

            if (@params.Filters != null)
            {
                query = query.Where(@params.Filters);

            }

            var result = await query.Include(user => user.Department)
                                    .Include(user => user.Job)
                                    .ToListAsync(cancellationToken);

            return new UserResult(result);
        }

        public async Task<UserResult> GetUserEventsAsync(UserParams @params, CancellationToken cancellationToken)
        {
            var query = _dbContext.Set<User>().AsQueryable();

            if (@params.Filters != null)
            {
                query = query.Where(@params.Filters);

            }

            var result = await query.Include(user => user.Events)
                                    .ToListAsync(cancellationToken);

            return new UserResult(result);
        }

        public void UpdateUser(User request)
        {
            var query = _dbContext.Set<User>().AsQueryable();

            if (request != null)
            {
                query = query.Where(@user => @user.Id == request.Id);

            }
            else
            {
                throw new ArgumentNullException("Invalid request");
            }

            if (query.Count() <= 0)
            {
                throw new NullReferenceException("Record not found");
            }

            var item = query.FirstOrDefault();

            if (request.Password == null)
            {
                request.Password = item.Password;
            }

            _dbContext.Update(request);
        }

        public void DeleteUser(UserParams @params)
        {
            var query = _dbContext.Set<User>().AsQueryable();

            if (@params.Filters != null)
            {
                query = query.Where(@params.Filters);

            }
            else
            {
                throw new ArgumentNullException("Invalid request");
            }

            if (query.Count() <= 0)
            {
                throw new NullReferenceException("Record not found");
            }

            var item = query.Include(user => user.Events).FirstOrDefault();

            _dbContext.Remove(item);
        }
    }
}
