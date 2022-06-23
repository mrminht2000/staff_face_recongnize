using System;

namespace StaffManagement.Core.Persistence.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; } 
        public int DepartmentId { get; set; }
        public int JobId { get; set; }
        public DateTime StartDay { get; set; }
        public bool IsConfirmed { get; set; } 
    }
}
