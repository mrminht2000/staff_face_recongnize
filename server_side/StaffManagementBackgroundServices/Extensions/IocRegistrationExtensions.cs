using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StaffManagement.BackgroundServices.Core.Persistence.Repositories;
using StaffManagement.BackgroundServices.Core.Services.Impls;
using StaffManagement.BackgroundServices.Core.Services.Interfaces;
using StaffManagement.BackgroundServices.Infras.Persistence;
using StaffManagement.BackgroundServices.Infras.Persistence.Repositories;
using System;

namespace StaffManagement.BackgroundServices.Extensions
{
    public static class IocRegistrationExtensions
    {
        private const string PersistenceConnectionKey = "MasterDb";

        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped(sp =>
            {
                return new MsSqlStaffManagementDbContext(configuration.GetConnectionString(PersistenceConnectionKey));
            });

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MsSqlStaffManagementDbContext>());

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
        }

        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEventService, EventService>();
        }
    }
}
