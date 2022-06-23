using StaffManagement.Core.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StaffManagement.Infras.Persistence.EntityTypeConfigurations
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
                .IsRequired()
                .HasColumnType("BIGINT");

            builder.Property(t => t.JobId)
                .IsRequired()
                .HasColumnType("BIGINT");

            builder.Property(t => t.StartDay)
                .IsRequired()
                .HasColumnType("DATETIME2(7)");

            builder.Property(t => t.IsConfirmed)
                .IsRequired()
                .HasColumnType("BOOLEAN");
        }
    }
}
