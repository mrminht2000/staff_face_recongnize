
using StaffManagement.API.Core.Common;
using StaffManagement.API.Core.Persistence.Models;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.API.Core.Persistence.Repositories
{
    public interface IUserRepository
    {
        void CreateUser(User @user);

        Task<UserResult> GetUserAsync(UserParams @param, CancellationToken cancellationToken);

        Task<UserResult> GetUserEventsAsync(UserParams @params, CancellationToken cancellationToken);

        void UpdateUser(User user);

        void DeleteUser(UserParams @params);
    }
}