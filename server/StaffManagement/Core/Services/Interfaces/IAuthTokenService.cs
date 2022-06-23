using StaffManagement.Core.Common;

namespace StaffManagement.Core.Services.Interfaces
{
    public interface IAuthTokenService
    {
        string GenerateToken(UserData data);

        bool VerifyToken(string token);

        string GetClaim(string token, string claimType);
    }
}
