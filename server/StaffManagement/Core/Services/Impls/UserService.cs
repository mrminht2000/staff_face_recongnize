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
    }
}
