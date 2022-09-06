using StaffManagement.Core.Core.Services.Dtos;
using System.Threading;
using System.Threading.Tasks;


namespace StaffManagement.Core.Core.Services.Interfaces
{
    public interface IAuthUserService
    {
        Task<string> QueryUserAsync(AuthUserRequest request, CancellationToken cancellationToken = default);
    }

}
