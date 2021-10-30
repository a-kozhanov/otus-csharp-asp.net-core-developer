using HotChocolate;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactoryGraphQL.Types
{
    public class CustomersQueries
    {
        public Task <IEnumerable<CustomerShortResponse>> GetCustomers([Service] Domain.ICustomers customers)
        {
            return customers.GetAllAsync();
        }

        public Task<CustomerResponse> GetCustomer([Service] Domain.ICustomers customers, Guid id)
        {
            return customers.GetByIdAsync(id);
        }

        public int AddCustomer([Service] Domain.ICustomers customers, CreateOrEditCustomerRequest request)
        {
            customers.AddAsync(request);
            return 200;
        }

        public int EditCustomer([Service] Domain.ICustomers customers, Guid id, CreateOrEditCustomerRequest request)
        {
            customers.UpdateAsync(id, request);
            return 200;
        }

        public int DeleteCustomer([Service] Domain.ICustomers customers, Guid id)
        {
            customers.DeleteAsync(id);
            return 200;
        }
    }
}
