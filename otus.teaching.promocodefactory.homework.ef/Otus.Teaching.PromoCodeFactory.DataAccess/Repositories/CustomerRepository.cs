using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class CustomerRepository : EfRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context)
        {
        }

        public async Task<ICollection<Customer>> GetAllAsync(Expression<Func<Customer, bool>> predicate)
        {
            return await DbSet.Include(c => c.PromoCodes).Include(c => c.CustomerPreferences).Where(predicate).ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            return await DbSet.Include(c => c.PromoCodes).AsNoTracking().Include(c => c.CustomerPreferences).Include("CustomerPreferences.Preference").
                FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> GetCustomerForUpdate(Guid id)
        {
            return await DbSet.Include(c => c.PromoCodes).Include(c => c.CustomerPreferences).
                FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
