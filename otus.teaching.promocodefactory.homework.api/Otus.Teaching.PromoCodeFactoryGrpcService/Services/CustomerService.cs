using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Mappers;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;
using Google.Protobuf.WellKnownTypes;

namespace Otus.Teaching.PromoCodeFactoryGrpcService
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly IRepository<PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer> _customerRepository;
        private readonly IRepository<Preference> _preferenceRepository;

        public CustomerService(ILogger<CustomerService> logger,
            IRepository<PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer> customerRepository,
            IRepository<Preference> preferenceRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _preferenceRepository = preferenceRepository;
        }

        public override async Task GetCustomers(Empty request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            var customers = await _customerRepository.GetAllAsync();

            var modelsData = (from l1 in customers.ToList()
              select new CustomerModel()
              {
                  Id = l1.Id.ToString(),
                  Email = l1.Email,
                  FirstName = l1.FirstName,
                  LastName = l1.LastName
              }).ToList();


            foreach (var product in modelsData)
            {
                await responseStream.WriteAsync(product);
            }
        }

        public override async Task GetCustomer(CustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            var customer = await _customerRepository.GetByIdAsync(Guid.Parse(request.Id));
            var customerModel = new CustomerModel()
            {
                Id = customer.Id.ToString(),
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName

            };
            await responseStream.WriteAsync(customerModel);
        }
      
        public override async Task<Empty> CreateCustomer(CustomerModel request,  ServerCallContext context)
        {
            List<Guid> guids = new List<Guid>
            {
                Guid.Parse(request.PreferenceId)
            };

            //Получаем предпочтения из бд и сохраняем большой объект
            _ = await _preferenceRepository.GetRangeByIdsAsync(guids);

            var createOrEditCustomerRequest = new PromoCodeFactory.Core.Domain.PromoCodeManagement.Customer()
            {
                
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName 
            };
        
            await _customerRepository.AddAsync(createOrEditCustomerRequest);

            return new Empty();
        }

        public override async Task<Empty> EditCustomers(CustomerModel request, ServerCallContext context)
        {
            var guids = new List<Guid>
            {
                Guid.Parse(request.PreferenceId)
            };

            var customer = await _customerRepository.GetByIdAsync(Guid.Parse(request.Id));

            var preferences = await _preferenceRepository.GetRangeByIdsAsync(guids);

            var createOrEditCustomerRequest = new CreateOrEditCustomerRequest()
            {
                FirstName = request.FirstName,
                Email = request.Email,
                LastName = request.LastName,
                PreferenceIds = guids
            };
            
            CustomerMapper.MapFromModel(createOrEditCustomerRequest, preferences, customer);
 
            await _customerRepository.UpdateAsync(customer);

            return new Empty();
        }

        public override async  Task<Empty> DeleteCustomers(CustomerRequest request, ServerCallContext context)
        {
            var customer = await _customerRepository.GetByIdAsync(Guid.Parse(request.Id));

            await _customerRepository.DeleteAsync(customer);

            return new Empty();
        }

    }
}
