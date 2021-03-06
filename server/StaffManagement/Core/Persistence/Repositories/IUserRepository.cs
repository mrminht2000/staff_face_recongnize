
using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Models;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Persistence.Repositories
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