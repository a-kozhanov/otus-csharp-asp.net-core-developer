using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.EntityConfigurations
{
    public class PromoCodeEntityConfiguration : IEntityTypeConfiguration<PromoCode>
    {

        public void Configure(EntityTypeBuilder<PromoCode> builder)
        {
            builder.ToTable("PromoCode");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Code).IsRequired().HasMaxLength(100);

            builder
               .HasOne<Employee>(s => s.PartnerManager)
               .WithMany(g => g.PromoCodes)
               .HasForeignKey(s => s.PartnerManagerId);

            builder
                .HasOne<Customer>(s => s.Customer)
                .WithMany(g => g.PromoCodes)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne<Preference>(s => s.Preference)
                .WithMany(g => g.PromoCodes)
                .HasForeignKey(s => s.PreferenceId);
        }
    }
}
