using System;

namespace StaffManagement.Core.ViewModels
{
    public class EventCreateReq
    {
        public string EventName { get; set; }

        public int EventType { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool AllDay { get; set; }

        public char? Per { get; set; }

        public bool IsConfirmed { get; set; }

        public long? UserId { get; set; }
    }
}
