using StaffManagement.Core.Persistence.Models;
using System.Collections.Generic;

namespace StaffManagement.ViewModels
{
    public class EventQueryResp: PagingQueryResp
    {
        public List<Event> Data { get; set; }

    }
}
