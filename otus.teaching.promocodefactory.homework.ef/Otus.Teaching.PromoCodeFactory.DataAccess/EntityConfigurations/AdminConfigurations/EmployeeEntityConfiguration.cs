using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.EntityConfigurations.AdminConfigurations
{
    public class EmployeeEntityConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employee");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(r => r.LastName).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Email).IsRequired().HasMaxLength(100);
            builder.Property(r => r.AppliedPromocodesCount).IsRequired().HasDefaultValue(0);

            builder
                .HasOne<Role>(s => s.Role)
                .WithMany(g => g.Employees)
                .HasForeignKey(s => s.RoleId);
        }
    }
}
