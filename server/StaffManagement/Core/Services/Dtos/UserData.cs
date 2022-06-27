using StaffManagement.Core.Persistence.Models;
using System;

namespace StaffManagement.Core.Services.Dtos
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
        public bool IsConfirmed { get; set; }

        public UserData(User user)
        {
            UserName = user.UserName;
            Id = user.Id;
            FullName = user.FullName;
            Role = user.Role;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            DepartmentId = user.DepartmentId;
            JobId = user.JobId;
            StartDay = user.StartDay;
            IsConfirmed = user.IsConfirmed;
        }
    }
}
