using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactoryGraphQL.Domain
{
    public interface ICustomers
    {
        Task<IEnumerable<CustomerShortResponse>> GetAllAsync();

        Task<CustomerResponse> GetByIdAsync(Guid id);

        Task AddAsync(CreateOrEditCustomerRequest entity);

        Task UpdateAsync(Guid id, CreateOrEditCustomerRequest request);

        Task DeleteAsync(Guid id);
    }
}