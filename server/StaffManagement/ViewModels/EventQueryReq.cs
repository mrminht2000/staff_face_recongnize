using System;
using System.Collections.Generic;

namespace StaffManagement.ViewModels
{
    public class EventQueryReq: PagingQueryReq
    {
        public int BranchId { get; set; } = -1;

        public List<string> AggregateId { get; set; }

    }
}
