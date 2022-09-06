using System;

namespace StaffManagement.Core.Core.Helpers
{
    public class EventHelpers
    {
        public (double, TimeSpan) CalculateWorkingTime(DateTime checkIn, DateTime checkOut)
        {
            var vnCheckIn = checkIn.AddHours(7);
            var vnCheckOut = checkOut.AddHours(7);

            var startTime = vnCheckIn.Date.AddHours(8); // 8h AM
            var endTime = vnCheckOut.Date.AddHours(18); // 6h PM

            if (checkIn == null && checkOut == null)
            {
                return (0, TimeSpan.FromSeconds(0));
            }

            var startLate = startTime < vnCheckIn ? vnCheckIn - startTime : TimeSpan.FromSeconds(0);

            if (checkOut == null)
            {
                return (0.5, startLate);
            }

            var endSoon = endTime > vnCheckOut ? endTime - vnCheckOut : TimeSpan.FromSeconds(0);

            return (1, startLate + endSoon);
        }
    }
}
