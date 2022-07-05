using System;
using System.Collections.Generic;

namespace StaffManagement.Core.Services.Dtos
{
    public class QueryEventRequest
    {
        public long? UserId { get; set; }
        public long? EventId { get; set; }
    }
}
