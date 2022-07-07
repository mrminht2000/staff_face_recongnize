using StaffManagement.Core.Services.Dtos;
using System.Threading;
using System.Threading.Tasks;
using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Models;

namespace StaffManagement.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(User @user, CancellationToken cancellationToken = default);

        Task<UserResult> QueryUsersAsync(CancellationToken cancellationToken = default);

        Task<UserResult> QueryUsersEventsAsync(CancellationToken cancellationToken = default);

        Task<UserData> QueryUserByIdAsync(QueryUserRequest request, CancellationToken cancellationToken = default);

        Task UpdateUserAsync(User @user, CancellationToken cancellationToken = default);

        Task DeleteUserAsync(QueryUserRequest request, CancellationToken cancellationToken = default);
    }

}
