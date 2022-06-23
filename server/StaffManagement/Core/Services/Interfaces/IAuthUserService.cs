using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Services.Dtos;
using System.Threading;
using System.Threading.Tasks;
using StaffManagement.Core.Common;


namespace StaffManagement.Core.Services.Interfaces
{
    public interface IAuthUserService
    {
        Task<string> QueryUserAsync(AuthUserRequest request, CancellationToken cancellationToken = default);
    }

}
