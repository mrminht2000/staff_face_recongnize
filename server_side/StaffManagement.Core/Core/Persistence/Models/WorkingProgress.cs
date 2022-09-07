using System;

namespace StaffManagement.Core.Core.Persistence.Models
{
    public class WorkingProgress
    {
        public long Id { get; set; }

        public double WorkingDayInMonth { get; set; }

        public double LateTimeByHours { get; set; }

        public DateTime LastUpdate { get; set; }

        public long UserId { get; set; }

        public virtual User User { get; set; }
    }
}
