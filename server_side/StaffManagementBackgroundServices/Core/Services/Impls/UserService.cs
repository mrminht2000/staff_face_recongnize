using StaffManagement.BackgroundServices.Core.Common;
using StaffManagement.BackgroundServices.Core.Persistence.Repositories;
using StaffManagement.BackgroundServices.Core.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.BackgroundServices.Core.Services.Impls
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<UserResult> QueryUsersAsync(CancellationToken cancellationToken = default)
        {
            var result = await _userRepository.GetUserAsync(new UserParams(), cancellationToken);

            return result;
        }
    }
}
