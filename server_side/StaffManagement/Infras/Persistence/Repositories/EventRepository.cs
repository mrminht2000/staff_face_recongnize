using StaffManagement.API.Core.Persistence.Models;
using StaffManagement.API.Core.Persistence.Repositories;

namespace StaffManagement.API.Infras.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(MsSqlStaffManagementDbContext dbContext)
            : base(dbContext)
        { }
    }
}
