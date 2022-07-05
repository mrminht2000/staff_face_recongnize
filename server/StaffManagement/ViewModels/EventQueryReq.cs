using System;
using System.Collections.Generic;

namespace StaffManagement.ViewModels
{
    public class EventQueryReq
    {
        public long? UserId { get; set; }
        public long? EventId { get; set; }
    }
}
