using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.DataAccess.EntityConfigurations;
using Otus.Teaching.PromoCodeFactory.DataAccess.EntityConfigurations.AdminConfigurations;

namespace Otus.Teaching.PromoCodeFactory.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<CustomerPreference> CustomerPreferences { get; set; }

        public DataContext()
        {
            // Database.EnsureDeletedAsync();
            // Database.EnsureDeletedAsync();
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PreferenceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PromoCodeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerPreferenceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}