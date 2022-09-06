using StaffManagement.Core.Core.Common;
using StaffManagement.Core.Core.Persistence.Models;
using StaffManagement.Core.Core.Persistence.Repositories;
using StaffManagement.Core.Core.Services.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.Core.Core.Services.Impls
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateDepartmentAsync(Department request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Bad request");
            }

            _departmentRepository.Create(request);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<Department> QueryDepartmentAsync(long id, CancellationToken cancellationToken = default)
        {
            Expression<Func<Department, bool>> filters = @department => id == @department.Id;

            var result = await _departmentRepository.GetAsync(new QueryParams<Department>(filters), cancellationToken);

            if (result == null || result.Data.Count == 0)
            {
                throw new NullReferenceException("Department not found");
            }

            return result.Data.FirstOrDefault();
        }

        public async Task<QueryResult<Department>> QueryDepartmentsAsync(CancellationToken cancellationToken = default)
        {
            var result = await _departmentRepository.GetAsync(new QueryParams<Department>(null), cancellationToken);

            return result;
        }

        public async Task UpdateDepartmentAsync(Department request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Department, bool>> filters = @department => request.Id == @department.Id;

            _departmentRepository.Update(new QueryParams<Department>(filters), request);

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task DeleteDepartmentAsync(long id, CancellationToken cancellationToken = default)
        {
            Expression<Func<Department, bool>> filters = @department => id == @department.Id;

            _departmentRepository.Delete(new QueryParams<Department>(filters));

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
