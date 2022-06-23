using System;

namespace StaffManagement.Core.Persistence.Models
{
    public class Event
    {
        public Guid EventId { get; set; }
        public string EventType { get; set; }
        public Guid AggregateId { get; set; }
        public string AggregateType { get; set; }
        public string RawData { get; set; }
        public long LocalTime { get; set; }
        public long LocalVersion { get; set; }
        public long OriginVersion { get; set; }
        public long TrackVersion { get; set; }
        public int TenantId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public string ReplicaInfo { get; set; }
        public DateTime CreatedDate { get; set; }
        public long Id { get; set; }
    }
}
