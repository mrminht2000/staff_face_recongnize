using System.Collections.Generic;

namespace StaffManagement.Core.Core.Persistence.Models
{
    public class Department
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
