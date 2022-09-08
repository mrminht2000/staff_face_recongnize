using StaffManagement.Core.Core.Common;
using StaffManagement.Core.Core.Helpers;
using StaffManagement.Core.Core.Persistence.Models;
using StaffManagement.Core.Core.Persistence.Repositories;
using StaffManagement.Core.Core.Services.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static StaffManagement.Core.Core.Common.Enum.EventEnum;

namespace StaffManagement.Core.Core.Services.Impls
{
    public class WorkingProgressService : IWorkingProgressService
    {
        private readonly IEventService _eventService;
        private readonly IWorkingProgressRepository _workingProgressRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WorkingProgressService(IWorkingProgressRepository workingProgressRepository, IEventService eventService , IUnitOfWork unitOfWork)
        {
            _workingProgressRepository = workingProgressRepository ?? throw new ArgumentNullException(nameof(workingProgressRepository));
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task UpdateWorkingDayAsync(long userId, CancellationToken cancellationToken = default)
        {
            var wp = await GetByUserIdAsync(userId, cancellationToken);

            var today = DateTime.UtcNow.AddHours(7).Date;

            var startDay = new DateTime(today.Year, today.Month, 1); // first day this month

            var workingProgress = new WorkingProgress
            {
                UserId = userId,
                WorkingDayInMonth = 0,
                LastUpdate = DateTime.Now,
                LateTimeByHours = 0
            };

            if (wp != null && wp.LastUpdate > startDay)
            {
                startDay = wp.LastUpdate.AddHours(7).Date;
                workingProgress.WorkingDayInMonth = wp.WorkingDayInMonth;
                workingProgress.LateTimeByHours = wp.LateTimeByHours;
            }

            while (startDay <= today)
            {
                var events = await _eventService.QueryEventsByUserIdAsync(new Dtos.QueryEventRequest { UserId = userId }, startDay, cancellationToken);

                DateTime? checkInEvent = null;
                DateTime? checkOutEvent = null;

                foreach (var userEvent in events.Data)
                {
                    if (userEvent.EventType == (int)EventType.Vacation)
                    {
                        workingProgress.WorkingDayInMonth++;
                        break;
                    }

                    if (userEvent.EventType == (int)EventType.Absent)
                    {
                        break;
                    }

                    if (userEvent.EventType == (int)EventType.Register && userEvent.EventName.Equals("Check-in")) {
                        checkInEvent = userEvent.StartTime;
                    }

                    if (userEvent.EventType == (int)EventType.Register && userEvent.EventName.Equals("Check-out"))
                    {
                        checkOutEvent = userEvent.StartTime;
                    }
                }

                var (workingTime, late) = EventHelpers.CalculateWorkingTime(checkInEvent, checkOutEvent);

                workingProgress.WorkingDayInMonth += workingTime;
                workingProgress.LateTimeByHours += late.TotalHours;

                startDay = startDay.AddDays(1);
            }

            if (wp == null)
            {
                _workingProgressRepository.Create(workingProgress);
            }
            else
            {
                workingProgress.Id = wp.Id;

                Expression<Func<WorkingProgress, bool>> filter = @workingProgress => @workingProgress.Id == workingProgress.Id;

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
