using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.EntityConfigurations
{
    public class CustomerPreferenceEntityConfiguration : IEntityTypeConfiguration<CustomerPreference>
    {
        public void Configure(EntityTypeBuilder<CustomerPreference> builder)
        {
            builder.ToTable("CustomerPreference");

            builder.HasKey(c => new {c.PreferenceId, c.CustomerId});

            builder
                .HasOne(c => c.Customer)
                .WithMany(c => c.CustomerPreferences)
                .HasForeignKey(c => c.CustomerId);

            builder
                .HasOne(c => c.Preference)
                .WithMany(c => c.CustomerPreferences)
                .HasForeignKey(c => c.PreferenceId);
        }
    }
}