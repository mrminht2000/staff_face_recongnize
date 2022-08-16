using StaffManagement.API.Core.Services.Dtos;
using System.Threading;
using System.Threading.Tasks;


namespace StaffManagement.API.Core.Services.Interfaces
{
    public interface IAuthUserService
    {
        Task<string> QueryUserAsync(AuthUserRequest request, CancellationToken cancellationToken = default);
    }

}
