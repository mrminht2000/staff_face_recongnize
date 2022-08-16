using StaffManagement.BackgroundServices.Core.Common;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.BackgroundServices.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResult> QueryUsersAsync(CancellationToken cancellationToken = default);
    }
}
