using StaffManagement.Core.Persistence.Repositories;
using StaffManagement.Core.Services.Impls;
using StaffManagement.Core.Services.Interfaces;
using StaffManagement.Infras.Persistence;
using StaffManagement.Infras.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StaffManagement.Core.Context;
using Microsoft.AspNetCore.Http;
using System;

namespace StaffManagement.Extensions
{
    public static class IocRegistrationExtensions
    {
        private const string PersistenceConnectionKey = "MasterDb";
        private const string UserIdKey = "Account";
        private const string UserRoleKey = "AccountRole";

        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped(sp =>
            {
                return new MsSqlStaffManagementDbContext(configuration.GetConnectionString(PersistenceConnectionKey));
            }); 
            
            /*
            services.AddDbContext<MsSqlStaffManagementDbContext>(options => 
            {
                options.UseSqlServer(configuration.GetConnectionString(PersistenceConnectionKey));
            });*/

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<MsSqlStaffManagementDbContext>());

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
        }

        public static void AddCoreService(this IServiceCollection services)
        {
            services.AddScoped<IAuthTokenService, JwtAuthService>();
            services.AddScoped<IAuthUserService, AuthUserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IJobService, JobService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
        }

        public static void AddAuthenticationContext(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationContext>(sp =>
            {
                var context = sp.GetRequiredService<IHttpContextAccessor>();

                if (context.HttpContext.Items.ContainsKey(UserIdKey) && context.HttpContext.Items.ContainsKey(UserRoleKey))
                {
                    var userId = int.Parse((string)context.HttpContext.Items[UserIdKey]);
                    var userRole = int.Parse((string)context.HttpContext.Items[UserRoleKey]);
                    return new AuthenticationContext(userId, userRole);
                }

                throw new UnauthorizedAccessException("Signing in is required");
            });
        }
    }
}
