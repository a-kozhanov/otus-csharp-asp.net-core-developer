using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerByIdAsync(Guid id);
        Task<Customer> GetCustomerForUpdate(Guid id);
        Task<ICollection<Customer>> GetAllAsync(Expression<Func<Customer, bool>> predicate);
    }
}