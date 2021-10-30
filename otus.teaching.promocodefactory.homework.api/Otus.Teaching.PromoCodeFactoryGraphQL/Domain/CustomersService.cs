using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Mappers;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactoryGraphQL.Domain
{
    public class CustomersService :
        ICustomers
    {

        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Preference> _preferenceRepository;

        public CustomersService(IRepository<Customer> customerRepository, IRepository<Preference> preferenceRepository)
        {
            _customerRepository = customerRepository;
            _preferenceRepository = preferenceRepository;
        }


        public async Task AddAsync(CreateOrEditCustomerRequest request)
        {
            //Получаем предпочтения из бд и сохраняем большой объект
            var preferences = await _preferenceRepository
                .GetRangeByIdsAsync(request.PreferenceIds);

            Customer customer = CustomerMapper.MapFromModel(request, preferences);

            await _customerRepository.AddAsync(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
      
            await _customerRepository.DeleteAsync(customer);
        }


        public async Task<CustomerResponse> GetByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            var response = new CustomerResponse(customer);

            return response;
        }

        public async Task<IEnumerable<CustomerShortResponse>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            var response = customers.Select(x => new CustomerShortResponse()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).ToList();

            return response;
        }

        public async Task UpdateAsync(Guid id, CreateOrEditCustomerRequest request)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            var preferences = await _preferenceRepository.GetRangeByIdsAsync(request.PreferenceIds);

            CustomerMapper.MapFromModel(request, preferences, customer);

            await _customerRepository.UpdateAsync(customer);

        }
    }
}
