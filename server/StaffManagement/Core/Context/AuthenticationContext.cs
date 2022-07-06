namespace StaffManagement.Core.Context
{
    public class AuthenticationContext : IAuthenticationContext
    {
        public int UserId { get; }

        public int UserRole { get; }

        public AuthenticationContext(int userId, int userRole)
        {
            UserId = userId;
            UserRole = userRole;
        }
    }
}
