using Otus.Teaching.PromoCodeFactory.DataAccess.Data;
using System.Linq;

namespace Otus.Teaching.PromoCodeFactory.DataAccess
{
    public static class DbInitializer
    {
        public static async void Initialize(DataContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                await dbContext.Roles.AddRangeAsync(FakeDataFactory.Roles);
                await dbContext.Employees.AddRangeAsync(FakeDataFactory.Employees);
                await dbContext.Preferences.AddRangeAsync(FakeDataFactory.Preferences);
                await dbContext.Customers.AddRangeAsync(FakeDataFactory.Customers);
                await dbContext.CustomerPreferences.AddRangeAsync(FakeDataFactory.CustomerPreferences);
                await dbContext.PromoCodes.AddRangeAsync(FakeDataFactory.PromoCodes);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}