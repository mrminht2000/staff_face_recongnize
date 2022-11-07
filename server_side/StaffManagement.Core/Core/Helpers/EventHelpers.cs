using StaffManagement.Core.Core.Persistence.Models;
using System;
using System.Collections.Generic;

namespace StaffManagement.Core.Core.Helpers
{
    public static class EventHelpers
    {
        public static int CountDays(List<Event> events)
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

        public static (double, TimeSpan) CalculateWorkingTime(DateTime? checkIn, DateTime? checkOut)
        {
            if (checkIn == null && checkOut == null)
            {
                return (0, TimeSpan.FromSeconds(0));
            }

            var vnCheckIn = checkIn;
            var startTime = vnCheckIn?.Date.AddHours(8); // 8h AM
            var startLate = startTime < vnCheckIn ? vnCheckIn - startTime : TimeSpan.FromSeconds(0);

            if (checkOut == null)
            {
                return (0.5, startLate ?? TimeSpan.FromSeconds(0));
            }

            var vnCheckOut = checkOut;
            var endTime = vnCheckOut?.Date.AddHours(18); // 6h PM
            var endSoon = endTime > vnCheckOut ? endTime - vnCheckOut : TimeSpan.FromSeconds(0);

            return (1, startLate + endSoon ?? TimeSpan.FromSeconds(0));
        }
    }
}
