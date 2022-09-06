using StaffManagement.Core.Core.Services.Dtos;

namespace StaffManagement.Core.Core.Services.Interfaces
{
    public interface IAuthTokenService
    {
        string GenerateToken(UserData data);

        bool VerifyToken(string token);

        string GetClaim(string token, string claimType);
    }
}
