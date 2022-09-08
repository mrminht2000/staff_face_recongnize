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
                var now = DateTime.Now;
                var hours = (22 - now.Hour >= 0) ? 22 - now.Hour : (22 + 24) - now.Hour;
                var minutes = 59 - now.Minute;
                var seconds = 59 - now.Second;
                var secondsTillWorking = hours * 3600 + minutes * 60 + seconds; // Execute at the end of the day

                secondsTillWorking = 0;
                _logger.LogInformation("Waiting {0:00}:{0:00}:{1:00} until working time", hours, minutes, seconds);
                await Task.Delay(TimeSpan.FromSeconds(secondsTillWorking), stoppingToken);


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

                            Console.WriteLine("Update user " + user.UserName + "'s register events successfully!");
                        } 
                    }

                    _logger.LogInformation("Waiting for the next day till working time");
                    await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
