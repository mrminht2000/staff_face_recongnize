using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Persistence.Repositories;

namespace StaffManagement.Infras.Persistence.Repositories
{
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        public JobRepository(MsSqlStaffManagementDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
