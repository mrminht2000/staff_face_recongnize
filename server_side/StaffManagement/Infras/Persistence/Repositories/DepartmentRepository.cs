using StaffManagement.API.Core.Persistence.Models;
using StaffManagement.API.Core.Persistence.Repositories;

namespace StaffManagement.API.Infras.Persistence.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MsSqlStaffManagementDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
