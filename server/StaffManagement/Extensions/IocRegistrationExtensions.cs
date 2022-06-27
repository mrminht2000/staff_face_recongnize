using StaffManagement.Core.Persistence.Repositories;
using StaffManagement.Core.Services.Impls;
using StaffManagement.Core.Services.Interfaces;
using StaffManagement.Infras.Persistence;
using StaffManagement.Infras.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StaffManagement.Extensions
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
        }

        public static void AddCoreService(this IServiceCollection services)
        {
            services.AddScoped<IAuthTokenService, JwtAuthService>();
            services.AddScoped<IAuthUserService, AuthUserService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
