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
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUnitOfWork _unitOfWork;

        public JobService(IJobRepository jobRepository, IUnitOfWork unitOfWork)
        {
            _jobRepository = jobRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateJobAsync(Job request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Bad request");
            }

            _jobRepository.Create(request);

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<Job> QueryJobAsync(long id, CancellationToken cancellationToken = default)
        {
            Expression<Func<Job, bool>> filters = @job => id == @job.Id;

            var result = await _jobRepository.GetAsync(new QueryParams<Job>(filters), cancellationToken);

            if (result == null || result.Data.Count == 0)
            {
                throw new NullReferenceException("Job not found");
            }

            return result.Data.FirstOrDefault();
        }

        public async Task<QueryResult<Job>> QueryJobsAsync(CancellationToken cancellationToken = default)
        {
            var result = await _jobRepository.GetAsync(new QueryParams<Job>(null), cancellationToken);

            return result;
        }

        public async Task UpdateJobAsync(Job request, CancellationToken cancellationToken = default)
        {
            Expression<Func<Job, bool>> filters = @job => request.Id == @job.Id;

            _jobRepository.Update(new QueryParams<Job>(filters), request);

            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task DeleteJobAsync(long id, CancellationToken cancellationToken = default)
        {
            Expression<Func<Job, bool>> filters = @job => id == @job.Id;

            _jobRepository.Delete(new QueryParams<Job>(filters));

            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
