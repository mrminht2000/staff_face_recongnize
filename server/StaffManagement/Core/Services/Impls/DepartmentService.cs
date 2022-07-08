using StaffManagement.Core.Common;
using StaffManagement.Core.Persistence.Models;
using StaffManagement.Core.Persistence.Repositories;
using StaffManagement.Core.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace StaffManagement.Core.Services.Impls
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task CreateDepartmentAsync(Department request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Bad request");
            }

            await _departmentRepository.CreateAsync(request, cancellationToken);
        }

        public async Task<Department> QueryDepartmentAsync(long id, CancellationToken cancellationToken = default)
        {
            Expression<Func<Department, bool>> filters = @department => id == @department.Id;

            var result = await _departmentRepository.GetValueAsync(new QueryParams<Department>(filters), cancellationToken);

            if (result == null || result.Data.Count == 0)
            {
                throw new NullReferenceException("Department not found");
            }

            return result.Data.FirstOrDefault();
        }

        public async Task<QueryResult<Department>> QueryDepartmentsAsync(CancellationToken cancellationToken = default)
        {
            var result = await _departmentRepository.GetValueAsync(new QueryParams<Department>(null), cancellationToken);

            return result;
        }

        public async Task UpdateDepartmentAsync(Department request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Department, bool>> filters = @department => request.Id == @department.Id;

            await _departmentRepository.UpdateAsync(new QueryParams<Department>(filters), request, cancellationToken);
        }

        public async Task DeleteDepartmentAsync(long id, CancellationToken cancellationToken = default)
        {
            Expression<Func<Department, bool>> filters = @department => id == @department.Id;

            await _departmentRepository.DeleteAsync(new QueryParams<Department>(filters), cancellationToken);
        }
    }
}
