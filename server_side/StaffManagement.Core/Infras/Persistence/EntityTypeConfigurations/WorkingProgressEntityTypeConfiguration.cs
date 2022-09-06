using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffManagement.Core.Core.Persistence.Models;

namespace StaffManagement.Core.Infras.Persistence.EntityTypeConfigurations
{
    public class WorkingProgressEntityTypeConfiguration : IEntityTypeConfiguration<WorkingProgress>
    {
        public void Configure(EntityTypeBuilder<WorkingProgress> builder)
        {
            builder.ToTable("WorkingProccess")
                .HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnType("BIGINT");

            builder.Property(t => t.WorkingDayInMonth)
                .HasColumnType("TINYINT");

            builder.Property(t => t.LateTimeByHours)
                .IsRequired()
                .HasColumnType("FLOAT");

            builder.Property(t => t.LastUpdate)
                .IsRequired()
                .HasColumnType("DATETIME2(7)");

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnType("BIGINT");

            builder.HasOne(i => i.User)
                .WithOne(d => d.WorkingProgress)
                .HasForeignKey<WorkingProgress>(e => e.UserId);
        }
    }
}
