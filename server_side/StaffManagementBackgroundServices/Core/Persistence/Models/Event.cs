using System;

namespace StaffManagement.BackgroundServices.Core.Persistence.Models
{
    public class Event
    {
        public long Id { get; set; }

        public string EventName { get; set; }

        public int EventType { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool AllDay { get; set; }

        public char? Per { get; set; }

        public bool IsConfirmed { get; set; }

        public long? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
