using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Persistence.Repositories;

namespace StaffManagement.Infras.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(MsSqlStaffManagementDbContext dbContext) 
            : base(dbContext)
        { }
    }
}
