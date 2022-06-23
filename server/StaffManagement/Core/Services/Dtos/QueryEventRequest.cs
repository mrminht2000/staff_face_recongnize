using System;
using System.Collections.Generic;

namespace StaffManagement.Core.Services.Dtos
{
    public class QueryEventRequest
    {
        public long BranchId { get; set; } = -1;

        public List<Guid> AggregateIds { get; set; }
        
        public string SortField { get; set; }

        public string SortDirection { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }
    }
}
