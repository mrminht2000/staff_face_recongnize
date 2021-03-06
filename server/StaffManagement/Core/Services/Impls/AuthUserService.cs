using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Persistence.Repositories;
using StaffManagement.Core.Services.Dtos;
using StaffManagement.Core.Services.Interfaces;
using StaffManagement.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Services.Impls
{
    public class AuthUserService : IAuthUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthTokenService _authTokenService;

        public AuthUserService(IUserRepository userRepository, IAuthTokenService authTokenService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authTokenService = authTokenService ?? throw new ArgumentNullException(nameof(authTokenService));
        }

        public async Task<string> QueryUserAsync(AuthUserRequest request, CancellationToken cancellationToken = default)
        {
            if (request.UserName == null || request.Password == null) 
                throw new ArgumentNullException("UserName and Password invalid");

            var passwordMd5 = request.Password.GenerateMD5();

            Expression<Func<User, bool>> filters = @user => request.UserName == @user.UserName && passwordMd5 == @user.Password;

            var result = await _userRepository.GetUserAsync(new UserParams(filters), cancellationToken);

            if (result.Users.Count == 0)
            {
                throw new UnauthorizedAccessException("Wrong Username or Password");
            }

            var currentUser = result.Users.First();

            return _authTokenService.GenerateToken(currentUser);
        }
    }
}
