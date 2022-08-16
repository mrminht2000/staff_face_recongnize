namespace StaffManagement.API.Core.Context
{
    public interface IAuthenticationContext
    {
        public int UserId { get; }

        public int UserRole { get; }
    }
}
