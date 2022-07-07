
using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Models;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Persistence.Repositories
{
    public interface IUserRepository 
    {
        Task CreateUserAsync(User @user, CancellationToken cancellationToken);

        Task<UserResult> GetUserAsync(UserParams @param, CancellationToken cancellationToken);

        Task<UserResult> GetUserEventsAsync(UserParams @params, CancellationToken cancellationToken);

        Task UpdateUserAsync(User user, CancellationToken cancellationToken);

        Task DeleteUserAsync(UserParams @params, CancellationToken cancellationToken);
    }
}