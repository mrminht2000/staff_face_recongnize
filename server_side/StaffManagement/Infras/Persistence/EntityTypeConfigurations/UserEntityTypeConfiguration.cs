using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffManagement.API.Core.Persistence.Models;

namespace StaffManagement.API.Infras.Persistence.EntityTypeConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users")
                .HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnType("BIGINT");

            builder.Property(t => t.UserName)
                .IsRequired()
                .HasColumnType("NVARCHAR(25)");

            builder.Property(t => t.Password)
                .IsRequired()
                .HasColumnType("NVARCHAR(50)");

            builder.Property(t => t.FullName)
                .IsRequired()
                .HasColumnType("NVARCHAR(50)");

            builder.Property(t => t.Role)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnType("TINYINT");

            builder.Property(t => t.PhoneNumber)
                .IsRequired()
                .HasColumnType("NVARCHAR(15)");

            builder.Property(t => t.Email)
                .IsRequired()
                .HasColumnType("NVARCHAR(50)");

            builder.Property(t => t.DepartmentId)
                .HasColumnType("BIGINT");

            builder.Property(t => t.JobId)
                .HasColumnType("BIGINT");

            builder.Property(t => t.StartDay)
                .IsRequired()
                .HasColumnType("DATETIME2(7)");

            builder.Property(t => t.IsConfirmed)
                .IsRequired()
                .HasColumnType("BIT");

            builder.HasOne(i => i.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(e => e.DepartmentId);

            builder.HasOne(i => i.Job)
                .WithMany(d => d.Users)
                .HasForeignKey(e => e.JobId);
        }
    }
}
