using System;

namespace StaffManagement.Core.Common
{
    public class UserData
    {
        public string UserName { get; set; } 

        public int Id { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public DateTime StartDay { get; set; }
        public int IsConfirmed { get; set; }
    }
}
