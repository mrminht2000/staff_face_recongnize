using StaffManagement.API.Core.Persistence.Models;
using StaffManagement.API.Core.Persistence.Repositories;

namespace StaffManagement.API.Infras.Persistence.Repositories
{
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        public JobRepository(MsSqlStaffManagementDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
