using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffManagement.Core.Core.Persistence.Models;

namespace StaffManagement.Core.Infras.Persistence.EntityTypeConfigurations
{
    public class EventEntityTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events")
                .HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnType("BIGINT");

            builder.Property(t => t.EventName)
                .IsRequired()
                .HasColumnType("NVARCHAR(50)");

            builder.Property(t => t.EventType)
                .IsRequired()
                .HasColumnType("TINYINT");

            builder.Property(t => t.StartTime)
                .IsRequired()
                .HasColumnType("DATETIME2(7)");

            builder.Property(t => t.EndTime)
                .HasColumnType("DATETIME2(7)");

            builder.Property(t => t.AllDay)
                .IsRequired()
                .HasColumnType("BIT");

            builder.Property(t => t.Per)
                .HasColumnType("CHAR(1)");

            builder.Property(t => t.IsConfirmed)
               .IsRequired()
               .HasColumnType("BIT");

            builder.Property(t => t.UserId)
                .HasColumnType("BIGINT");

            builder.HasOne(i => i.User)
                .WithMany(d => d.Events)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
