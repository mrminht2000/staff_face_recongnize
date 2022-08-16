namespace StaffManagement.BackgroundServices.Core.Services.Dtos
{
    public class QueryEventRequest
    {
        public long? UserId { get; set; }
        public long? EventId { get; set; }
    }
}
