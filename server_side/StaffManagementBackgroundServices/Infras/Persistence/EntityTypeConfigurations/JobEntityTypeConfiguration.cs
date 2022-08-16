using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffManagement.BackgroundServices.Core.Persistence.Models;

namespace StaffManagement.BackgroundServices.Infras.Persistence.EntityTypeConfigurations
{
    public class JobEntityTypeConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs")
                .HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnType("BIGINT");

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR(50)");

            builder.Property(t => t.Description)
                .HasColumnType("NVARCHAR(MAX)");

            builder.Property(t => t.Salary)
                .IsRequired()
                .HasColumnType("BIGINT");

            builder.Property(t => t.SalaryPer)
                .IsRequired()
                .HasColumnType("CHAR(1)");
        }
    }
}
