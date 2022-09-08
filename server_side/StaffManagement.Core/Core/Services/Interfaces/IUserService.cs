using StaffManagement.Core.Core.Common;
using StaffManagement.Core.Core.Persistence.Models;
using StaffManagement.Core.Core.Services.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(User @user, CancellationToken cancellationToken = default);

        Task<UserResult> QueryUsersAsync(CancellationToken cancellationToken = default);

        Task<UserResult> QueryUsersEventsAsync(CancellationToken cancellationToken = default);

        Task<UserData> QueryUserByIdAsync(QueryUserRequest request, CancellationToken cancellationToken = default);

        Task<UserResult> QueryUserWorkingDaysAsync(CancellationToken cancellationToken = default);

        Task<UserResult> QueryUserWorkingStatusAsync(CancellationToken cancellationToken = default);

        Task UpdateUserAsync(User @user, CancellationToken cancellationToken = default);

        Task DeleteUserAsync(QueryUserRequest request, CancellationToken cancellationToken = default);
    }

}
