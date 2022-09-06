using StaffManagement.Core.Core.Persistence.Models;
using System;
using System.Collections.Generic;

namespace StaffManagement.Core.Extensions
{
    public static class EventExtensions
    {
        public static int CountDays(this List<Event> events)
        {
            int count = 0;
            foreach (Event e in events)
            {
                if (e.EndTime == null)
                {
                    count++;
                    continue;
                }

                var daysBetween = ((DateTime)e.EndTime - (DateTime)e.StartTime).TotalDays;

                count += (int)daysBetween;
            }

            return count;
        }
    }
}
