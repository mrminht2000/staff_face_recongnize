using System.Collections.Generic;

namespace StaffManagement.Core.Persistence.Models
{
    public class Department
    {
        public long Id { get; set; }
        
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
