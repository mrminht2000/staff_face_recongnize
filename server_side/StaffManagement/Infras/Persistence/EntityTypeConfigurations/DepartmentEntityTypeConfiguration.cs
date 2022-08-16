using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StaffManagement.API.Core.Persistence.Models;

namespace StaffManagement.API.Infras.Persistence.EntityTypeConfigurations
{
    public class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments")
                .HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnType("BIGINT");

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR(50)");
        }
    }
}
