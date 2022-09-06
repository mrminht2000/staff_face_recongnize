using StaffManagement.Core.Core.Persistence.Models;
using StaffManagement.Core.Core.Persistence.Repositories;

namespace StaffManagement.Core.Infras.Persistence.Repositories
{
    public class WorkingProgressRepository : BaseRepository<WorkingProgress>, IWorkingProgressRepository
    {
        public WorkingProgressRepository(MsSqlStaffManagementDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
