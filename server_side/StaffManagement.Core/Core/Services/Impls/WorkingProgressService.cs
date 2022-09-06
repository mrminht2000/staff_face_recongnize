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
    public class WorkingProgressService : IWorkingProgressService
    {
        private readonly IWorkingProgressRepository _workingProgressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WorkingProgressService(IWorkingProgressRepository workingProgressRepository, IUnitOfWork unitOfWork)
        {
            _workingProgressRepository = workingProgressRepository ?? throw new ArgumentNullException(nameof(workingProgressRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task CreateOrUpdateAsync(WorkingProgress workingProgress, CancellationToken cancellationToken = default)
        {
            var wp = await GetByUserIdAsync(workingProgress.UserId, cancellationToken);

            if (wp == null)
            {
                _workingProgressRepository.Create(workingProgress);
            }
            else
            {
                Expression<Func<WorkingProgress, bool>> filter = @workingProgress => @workingProgress.UserId == workingProgress.UserId;

                _workingProgressRepository.Update(new QueryParams<WorkingProgress>(filter), workingProgress);
            }

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<WorkingProgress> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            Expression<Func<WorkingProgress, bool>> filter = @workingProgress => @workingProgress.Id == id;

            var result = await _workingProgressRepository.GetAsync(new QueryParams<WorkingProgress>(filter), cancellationToken);

            return result.Data.FirstOrDefault();
        }

        public async Task<WorkingProgress> GetByUserIdAsync(long userId, CancellationToken cancellationToken = default)
        {
            Expression<Func<WorkingProgress, bool>> filter = @workingProgress => @workingProgress.UserId == userId;

            var result = await _workingProgressRepository.GetAsync(new QueryParams<WorkingProgress>(filter), cancellationToken);

            return result.Data.FirstOrDefault();
        }
    }
}
