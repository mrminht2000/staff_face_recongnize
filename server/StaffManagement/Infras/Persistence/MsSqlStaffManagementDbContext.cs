using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StaffManagement.Core.Persistence.Repositories;
using System.Threading;
using StaffManagement.Infras.Persistence.EntityTypeConfigurations;
using StaffManagement.Core.Persistence.Models;

namespace StaffManagement.Infras.Persistence
{
    public class MsSqlStaffManagementDbContext : DbContext, IUnitOfWork
    {
        public MsSqlStaffManagementDbContext(string connectionString)
            : base(GetOptions(connectionString))
        {
        }

        public MsSqlStaffManagementDbContext(DbContextOptions<MsSqlStaffManagementDbContext> options) 
            : base(options)
        { }

        private static DbContextOptions GetOptions(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            return new DbContextOptionsBuilder()
                .UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public Task CommitAsync(CancellationToken cancellationToken)
        {
            return SaveChangesAsync(cancellationToken);
        }
    }
}
