using System;
using System.Collections.Generic;

namespace StaffManagement.Core.Persistence.Models
{
    public class User
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string? Password { get; set; }

        public string FullName { get; set; }

        public int Role { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; } 

        public long? DepartmentId { get; set; }

        public long? JobId { get; set; }

        public DateTime StartDay { get; set; }

        public bool IsConfirmed { get; set; } 

        public virtual ICollection<Event> Events { get; set; }

        public virtual Department? Department { get; set; }

        public virtual Job? Job { get; set; }
    }
}
