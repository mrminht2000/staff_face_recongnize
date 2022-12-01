using StaffManagement.Core.Core.Common;
using StaffManagement.Core.Core.Context;
using StaffManagement.Core.Core.Persistence.Models;
using StaffManagement.Core.Core.Persistence.Repositories;
using StaffManagement.Core.Core.Services.Dtos;
using StaffManagement.Core.Core.Services.Interfaces;
using StaffManagement.Core.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static StaffManagement.Core.Core.Common.Enum.EventEnum;
using static StaffManagement.Core.Core.Common.Enum.UserEnum;

namespace StaffManagement.Core.Core.Services.Impls
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventService _eventService;
        private readonly IAuthenticationContext _authenContext;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IEventService eventService, IAuthenticationContext authenContext, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _eventService = eventService;
            _authenContext = authenContext;
            _unitOfWork = unitOfWork;
        }

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUserAsync(User @user, CancellationToken cancellationToken = default)
        {
            if (_authenContext.UserRole <= @user.Role)
            {
                throw new Exception("You don't have enough permission");
            }

            if (@user == null)
            {
                throw new ArgumentNullException(nameof(@user));
            }

            user.Password = @user.Password.GenerateMD5();
            _userRepository.CreateUser(@user);
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

            var dateNow = DateTime.UtcNow.Date;

            var result = users.Users.Select(u =>
            {
                var user = u;
                user.Events = user.Events.Where(e =>
                    e.IsConfirmed == true &&
                    e.EventType != (int)EventType.Default &
                    ((e.EndTime == null && e.StartTime.Date == dateNow) ||
                    (e.EndTime != null && e.StartTime.Date <= dateNow && e.EndTime.GetValueOrDefault().Date >= dateNow))
                ).ToList();

                var absentCount = user.Events.Where(e => e.EventType == (int)EventType.Vacation || e.EventType == (int)EventType.Absent).Count();

                if (absentCount > 0)
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

        public async Task<UserResult> QueryUserWorkingDaysAsync(CancellationToken cancellationToken = default)
        {
            var users = await QueryUsersAsync(cancellationToken);

            var result = users.Users.Select(async u =>
            {
                var userEvents = await _eventService.QueryMonthEventsByUserId(u.Id);


            });

            throw new ArgumentNullException();
        }
        public async Task<UserResult> QueryUserWorkingProgressAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetUserWorkingProgressAsync(new UserParams(), cancellationToken);

            return users;
        }

        public async Task UpdateUserAsync(User request, CancellationToken cancellationToken = default)
        {
            if (_authenContext.UserRole <= request.Role)
            {
                throw new Exception("You don't have enough permission");
            }

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

            _userRepository.UpdateUser(request);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default)
        {
            Expression<Func<User, bool>> filters = @user => request.UserId == @user.Id;

            var result = await _userRepository.GetUserAsync(new UserParams(filters), cancellationToken);
            
            if (result == null)
            {
                throw new ArgumentNullException("User cannot be null");
            }

            var user = result.BaseUsers.FirstOrDefault();

            if (_authenContext.UserRole < user.Role)
            {
                throw new Exception("You don't have enough permission");
            }

            if (_authenContext.UserRole == user.Role && _authenContext.UserId != user.Id)
            {
                throw new Exception("You don't have enough permission");
            }

            if (!request.NewPassword.Equals(request.RePassword))
            {
                throw new Exception("RePassword must match New Password");
            }
            
            if (_authenContext.UserId == user.Id)
            {
                if (!request.OldPassword.GenerateMD5().Equals(user.Password))
                {
                    throw new Exception("Old Password is wrong");
                }
            }


            user.Password = request.NewPassword.GenerateMD5();
            _userRepository.UpdateUser(user);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task DeleteUserAsync(QueryUserRequest request, CancellationToken cancellationToken = default)
        {
            var user = await QueryUserByIdAsync(request, cancellationToken);
            if (_authenContext.UserRole <= user.Role)
            {
                throw new Exception("You don't have enough permission");
            }

            Expression<Func<User, bool>> filters = @user => request.Id == @user.Id;

            _userRepository.DeleteUser(new UserParams(filters));
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
