using StaffManagement.Core.Core.Persistence.Models;
using StaffManagement.Core.Core.Persistence.Repositories;

namespace StaffManagement.Core.Infras.Persistence.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MsSqlStaffManagementDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
