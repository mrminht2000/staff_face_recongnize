using Microsoft.EntityFrameworkCore;
using StaffManagement.Core.Core.Persistence.Models;
using StaffManagement.Core.Core.Persistence.Repositories;
using StaffManagement.Core.Infras.Persistence.EntityTypeConfigurations;
using System;
using System.Threading;
using System.Threading.Tasks;
using static StaffManagement.Core.Core.Common.Enum.EventEnum;

namespace StaffManagement.Core.Infras.Persistence
{
    public class MsSqlStaffManagementDbContext : DbContext, IUnitOfWork
    {
        public MsSqlStaffManagementDbContext(string connectionString)
            : base(GetOptions(connectionString))
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public MsSqlStaffManagementDbContext(DbContextOptions<MsSqlStaffManagementDbContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

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

        public DbSet<Event> Events { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<WorkingProgress> WorkingProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityTypeConfiguration).Assembly);

            DataSeeding(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void DataSeeding(ModelBuilder modelBuilder)
        {
            var rand = new Random();

            // Departments
            for (int i = 1; i <= 5; i++)
            {
                modelBuilder.Entity<Department>().HasData(new Department
                {
                    Id = i,
                    Name = "Department " + i.ToString()
                });
            }

            // Jobs
            for (int i = 1; i <= 5; i++)
            {
                modelBuilder.Entity<Job>().HasData(new Job
                {
                    Id = i * 2 - 1,
                    Name = "JobM" + i.ToString(),
                    Description = "Job Salary Per Month Example No." + i.ToString(),
                    Salary = rand.Next(1000, 9999),
                    SalaryPer = 'm'
                });

                modelBuilder.Entity<Job>().HasData(new Job
                {
                    Id = i * 2,
                    Name = "JobD" + i.ToString(),
                    Description = "Job Salary Per D Example No." + i.ToString(),
                    Salary = rand.Next(100, 999),
                    SalaryPer = 'd'
                });
            }

            // Users
            for (var i = 1; i <= 5; i++)
            {
                modelBuilder.Entity<User>().HasData(new User
                {
                    Id = i * 2 - 1,
                    UserName = "user" + i.ToString(),
                    Password = "ee11cbb19052e40b07aac0ca060c23ee", // user
                    Email = "user" + i.ToString() + "@example",
                    FullName = "User No." + i.ToString(),
                    Role = 0,
                    PhoneNumber = "000000000" + i.ToString(),
                    DepartmentId = rand.Next(1, 5),
                    JobId = rand.Next(1, 10),
                    StartDay = new DateTime(),
                    IsConfirmed = true,
                });

                modelBuilder.Entity<User>().HasData(new User
                {
                    Id = i * 2,
                    UserName = "admin" + i.ToString(),
                    Password = "21232f297a57a5a743894a0e4a801fc3", // admin
                    Email = "admin" + i.ToString() + "@example",
                    FullName = "Admin No. " + i.ToString(),
                    Role = 1,
                    PhoneNumber = "100000000" + i.ToString(),
                    DepartmentId = rand.Next(1, 5),
                    JobId = rand.Next(1, 10),
                    StartDay = new DateTime(),
                    IsConfirmed = true,
                });
            }

            // Events
            var today = DateTime.Now.Date;

            var firstDayofLastMonth = new DateTime(today.Year, (today.Month - 1 > 0 ? today.Month - 1 : 1), 1); // first day this month
            long eventId = 1;

            for (var i = 1; i <= 10; i++)
            {
                var startDay = firstDayofLastMonth;
                var count = 1;
                while (startDay <= today.AddDays(10) && count < 61) 
                {
                    var eventChoice = rand.Next(1, 10);
                    if (eventChoice <= 2)
                    {
                        modelBuilder.Entity<Event>().HasData(new Event
                        {
                            Id = eventId,
                            EventName = "Nghi khong luong",
                            EventType = (int)EventType.Absent,
                            StartTime = startDay.AddHours(-7),
                            AllDay = true,
                            IsConfirmed = true,
                            UserId = i
                        });

                        eventId++;
                    }

                    if (eventChoice > 2 && eventChoice <= 3)
                    {
                        modelBuilder.Entity<Event>().HasData(new Event
                        {
                            Id = eventId,
                            EventName = "Nghi co luong",
                            EventType = (int)EventType.Vacation,
                            StartTime = startDay.AddHours(-7),
                            AllDay = true,
                            IsConfirmed = true,
                            UserId = i
                        });

                        eventId++;
                    }

                    if (eventChoice > 4)
                    {
                        modelBuilder.Entity<Event>().HasData(new Event
                        {
                            Id = eventId,
                            EventName = "Check-in",
                            EventType = (int)EventType.Register,
                            StartTime = startDay.AddHours(rand.Next(7, 9) - 7),
                            AllDay = false,
                            IsConfirmed = true,
                            UserId = i
                        });

                        eventId++;

                        modelBuilder.Entity<Event>().HasData(new Event
                        {
                            Id = eventId,
                            EventName = "Check-out",
                            EventType = (int)EventType.Register,
                            StartTime = startDay.AddHours(rand.Next(17, 19) - 7),
                            AllDay = false,
                            IsConfirmed = true,
                            UserId = i
                        });

                        eventId++;
                    }

                    startDay = startDay.AddDays(1);
                    count++;
                }
            }
        }

        public Task CommitAsync(CancellationToken cancellationToken)
        {
            return SaveChangesAsync(cancellationToken);
        }
    }
}
