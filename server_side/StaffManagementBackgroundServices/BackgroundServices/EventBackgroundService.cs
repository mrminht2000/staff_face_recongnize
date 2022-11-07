using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StaffManagement.Core.Core.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.BackgroundServices.BackgroundServices
{
    public class EventBackgroundService : BackgroundService
    {
        private IEventService _eventService;
        private IUserService _userService;
        private IWorkingProgressService _workingProgressService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EventBackgroundService> _logger;

        public EventBackgroundService(IServiceProvider serviceProvider, ILogger<EventBackgroundService> logger)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                    using (IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        _logger.LogInformation("Event worker is starting .....");

                        _userService = scope.ServiceProvider.GetService<IUserService>();
                        _eventService = scope.ServiceProvider.GetService<IEventService>();
                        _workingProgressService = scope.ServiceProvider.GetService<IWorkingProgressService>();

                        var users = await _userService.QueryUsersAsync();

                        foreach (var user in users.Users)
                        {
                            await _eventService.ProccessRegisterEventsByUserIdAsync(user.Id);
                            await _workingProgressService.UpdateWorkingDayAsync(user.Id);

                            _logger.LogInformation("Update user " + user.UserName + "'s register events successfully!");
                        } 
                    }

                    var secondsTillWorking = TimeWaitUntilWorking();

                    _logger.LogInformation("The next task is working at {0:dd/MM/yyyy HH:mm:ss}", DateTime.Now.AddSeconds(secondsTillWorking));
                    
                    await Task.Delay(TimeSpan.FromSeconds(TimeWaitUntilWorking()), stoppingToken);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private double TimeWaitUntilWorking()
        {
            var now = DateTime.Now;
            var todayWorkingTime = now.Date.AddHours(22);
            double secondsTillWorking = 0;
            if (DateTime.Compare(now, todayWorkingTime) >= 0)
            {
                secondsTillWorking = todayWorkingTime.AddDays(1).Subtract(now).TotalSeconds;
            }
            else
            {
                secondsTillWorking = todayWorkingTime.Subtract(now).TotalSeconds;
            }

            return secondsTillWorking;
        }
    }
}
