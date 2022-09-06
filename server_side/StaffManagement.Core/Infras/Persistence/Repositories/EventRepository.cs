using StaffManagement.Core.Core.Persistence.Models;
using StaffManagement.Core.Core.Persistence.Repositories;

namespace StaffManagement.Core.Infras.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(MsSqlStaffManagementDbContext dbContext)
            : base(dbContext)
        { }
    }
}
