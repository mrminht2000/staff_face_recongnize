using StaffManagement.Core.Core.Common;
using StaffManagement.Core.Core.Persistence.Models;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Core.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task CreateDepartmentAsync(Department request, CancellationToken cancellationToken = default);

        Task<Department> QueryDepartmentAsync(long id, CancellationToken cancellationToken = default);

        Task<QueryResult<Department>> QueryDepartmentsAsync(CancellationToken cancellationToken = default);

        Task UpdateDepartmentAsync(Department request, CancellationToken cancellationToken = default);

        Task DeleteDepartmentAsync(long id, CancellationToken cancellationToken = default);
    }
}
