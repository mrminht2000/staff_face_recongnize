using System.Collections.Generic;

namespace StaffManagement.Core.Core.Persistence.Models
{
    public class Job
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long Salary { get; set; }

        public char SalaryPer { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
