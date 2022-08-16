using StaffManagement.API.Core.Services.Dtos;

namespace StaffManagement.API.Core.Services.Interfaces
{
    public interface IAuthTokenService
    {
        string GenerateToken(UserData data);

        bool VerifyToken(string token);

        string GetClaim(string token, string claimType);
    }
}
