using StaffManagement.Core.Common;
using StaffManagement.Core.Context;
using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Persistence.Repositories;
using StaffManagement.Core.Services.Dtos;
using StaffManagement.Core.Services.Interfaces;
using StaffManagement.Extensions;
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
        private readonly IAuthenticationContext _authenContext;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserRepository userRepository, IAuthenticationContext authenContext, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _authenContext = authenContext;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUserAsync(User @user, CancellationToken cancellationToken = default)
        {
            if (@user == null)
            {
                throw new ArgumentNullException(nameof(@user));
            }

            await _userRepository.CreateUserAsync(@user, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
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

            var dateNow = DateTime.Now.Date.AddHours(-7);

            var result = users.Users.Select(u =>
            {
                var user = u;
                user.Events = user.Events.Where(e =>
                    e.IsConfirmed == true &&
                    e.EventType != (int)EventType.Default &&
                    ((e.EndTime == null && e.StartTime == dateNow) ||
                    (e.EndTime != null && e.StartTime <= dateNow && e.EndTime.GetValueOrDefault() >= dateNow))
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

        public async Task UpdateUserAsync(User request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Password != null)
            {
                request.Password = request.Password.GenerateMD5();
            }

            if (_authenContext.UserRole < (int)Role.Admin)
            {
                request.IsConfirmed = false;
            }

            await _userRepository.UpdateUserAsync(request, cancellationToken);
        }

        public async Task DeleteUserAsync(QueryUserRequest request, CancellationToken cancellationToken = default)
        {
            Expression<Func<User, bool>> filters = @user => request.Id == @user.Id;

            await _userRepository.DeleteUserAsync(new UserParams(filters), cancellationToken);
        }
    }
}
