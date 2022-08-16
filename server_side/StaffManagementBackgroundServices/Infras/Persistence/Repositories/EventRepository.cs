using StaffManagement.BackgroundServices.Core.Persistence.Models;
using StaffManagement.BackgroundServices.Core.Persistence.Repositories;
using StaffManagement.BackgroundServices.Infras.Persistence;

namespace StaffManagement.BackgroundServices.Infras.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(MsSqlStaffManagementDbContext dbContext)
            : base(dbContext)
        { }
    }
}
