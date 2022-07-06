namespace StaffManagement.Core.Context
{
    public interface IAuthenticationContext
    {
        public int UserId { get; }

        public int UserRole { get; }
    }
}
