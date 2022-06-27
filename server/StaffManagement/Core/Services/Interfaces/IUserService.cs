using StaffManagement.Core.Services.Dtos;
using System.Threading;
using System.Threading.Tasks;
using StaffManagement.Core.Common;


namespace StaffManagement.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResult> QueryUsersAsync(CancellationToken cancellationToken = default);

        Task<UserData> QueryUserByIdAsync(QueryUserRequest request, CancellationToken cancellationToken = default);
    }

}
