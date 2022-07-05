using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Persistence.Repositories;
using StaffManagement.Core.Services.Dtos;
using StaffManagement.Core.Services.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static StaffManagement.Core.Common.Enum.EventEnum;
using static StaffManagement.Core.Common.Enum.UserEnum;

namespace StaffManagement.Core.Services.Impls
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserData> QueryUserByIdAsync(QueryUserRequest request, CancellationToken cancellationToken = default)
        {
            Expression<Func<User, bool>> filters = @user => request.Id == @user.Id;

            var result = await _userRepository.GetUserAsync(new UserParams(filters), cancellationToken);

            if (result.Users.Count == 0)
            {
                throw new NullReferenceException("User not found");
            }

            var user = result.Users.First();

            return user;
        }

        public async Task<UserResult> QueryUsersAsync(CancellationToken cancellationToken = default)
        {
            var result = await _userRepository.GetUserAsync(new UserParams(), cancellationToken);

            return result;
        }

        public async Task<UserResult> QueryUsersEventsAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetUserEventsAsync(new UserParams(), cancellationToken);

            var dateNow = DateTime.Now.Date;

            var result = users.Users.Select(u =>
            {
                var user = u;
                user.Events = user.Events.Where(e =>
                    e.IsConfirmed == true &&
                    e.EventType != (int)EventType.Default &&
                    ((e.EndTime == null && e.StartTime.Date == dateNow) ||
                    (e.EndTime != null && e.StartTime.Date <= dateNow && e.EndTime.GetValueOrDefault() >= dateNow))
                ).ToList();

                var absentCount = user.Events.Where(e => e.EventType == (int)EventType.Vacation || e.EventType == (int)EventType.Absent).Count();
                
                if( absentCount > 0 )
                {
                    user.Status = (int)Status.Absent;
                    return user;
                }

                var registerCount = user.Events.Where(e => e.EventType == (int)EventType.Register).Count();

                if (registerCount > 0)
                {
                    user.Status = (int)Status.Working;
                    return user;
                }

                user.Status = (int)Status.Unregister;
                return user;
            }).ToList();

            return new UserResult(result);
        }
    }
}
