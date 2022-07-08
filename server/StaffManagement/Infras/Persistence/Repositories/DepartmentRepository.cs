using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Persistence.Repositories;

namespace StaffManagement.Infras.Persistence.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MsSqlStaffManagementDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
