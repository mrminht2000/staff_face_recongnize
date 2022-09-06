using StaffManagement.Core.Core.Persistence.Models;
using StaffManagement.Core.Core.Persistence.Repositories;

namespace StaffManagement.Core.Infras.Persistence.Repositories
{
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        public JobRepository(MsSqlStaffManagementDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
