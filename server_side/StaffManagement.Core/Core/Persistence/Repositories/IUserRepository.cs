
using StaffManagement.Core.Core.Common;
using StaffManagement.Core.Core.Persistence.Models;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Core.Persistence.Repositories
{
    public interface IUserRepository
    {
        void CreateUser(User @user);

        Task<UserResult> GetUserAsync(UserParams @param, CancellationToken cancellationToken);

        Task<UserResult> GetUserEventsAsync(UserParams @userParams, CancellationToken cancellationToken);

        void UpdateUser(User user);

        void DeleteUser(UserParams @params);
    }
}