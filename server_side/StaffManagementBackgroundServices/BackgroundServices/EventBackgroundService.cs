using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StaffManagement.BackgroundServices.Core.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StaffManagement.BackgroundServices.BackgroundServices
{
    public class EventBackgroundService : BackgroundService
    {
        private IEventService _eventService;
        private IUserService _userService;
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
                        _userService = scope.ServiceProvider.GetService<IUserService>();
                        _eventService = scope.ServiceProvider.GetService<IEventService>();

                        var users = await _userService.QueryUsersAsync();

                        foreach (var user in users.Users)
                        {
                            await _eventService.DeleteRegisterEventsByUserId(user.Id);

                            Console.WriteLine("Delete user " + user.UserName + "'s unused events successfully!");
                        }
                    }
                    await WaitForNextSchedule();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private async Task WaitForNextSchedule()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}
