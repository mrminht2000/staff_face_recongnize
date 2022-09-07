using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StaffManagement.Core.Core.Context;
using StaffManagement.Core.Core.Persistence.Repositories;
using StaffManagement.Core.Core.Services.Impls;
using StaffManagement.Core.Core.Services.Interfaces;
using StaffManagement.Core.Infras.Persistence;
using StaffManagement.Core.Infras.Persistence.Repositories;
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
            services.AddScoped<IWorkingProgressRepository, WorkingProgressRepository>();
        }

        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IWorkingProgressService, WorkingProgressService>();
            services.AddScoped<IAuthenticationContext>( sp => new AuthenticationContext(0 , 0));
        }
    }
}
