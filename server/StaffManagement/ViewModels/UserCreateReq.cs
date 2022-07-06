namespace StaffManagement.ViewModels
{
    public class UserCreateReq
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public int Role { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public long? DepartmentId { get; set; }

        public long? JobId { get; set; }
    }
}
