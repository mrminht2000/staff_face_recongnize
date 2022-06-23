
using StaffManagement.Core.Common;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Persistence.Repositories
{
    public interface IUserRepository 
    {
        Task<UserResult> GetUserAsync(UserParams @param, CancellationToken cancellationToken);
    }
}