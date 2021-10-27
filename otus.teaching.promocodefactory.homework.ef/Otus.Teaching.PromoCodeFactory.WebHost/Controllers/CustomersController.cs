using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.WebHost.Models;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Клиенты
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerShortResponse>>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            return Ok(_mapper.Map<List<CustomerShortResponse>>(customers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound();

            var result = _mapper.Map<CustomerResponse>(customer);

            if (customer.CustomerPreferences != null)
            {
                var preferences = customer.CustomerPreferences.Where(c => c.CustomerId == customer.Id).Select(p => p.Preference).ToList();
                var preferenceResponse = _mapper.Map<List<PreferenceResponse>>(preferences);
                result.Preferences = preferenceResponse;
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CreateOrEditCustomerRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request is empty");
            }

            var customer = _mapper.Map<Customer>(request);
            customer.Id = Guid.NewGuid();

            if (request.PreferenceIds.Count > 0)
            {
                customer.CustomerPreferences = new List<CustomerPreference>();
                foreach (var item in request.PreferenceIds)
                {
                    customer.CustomerPreferences.Add(new CustomerPreference
                    {
                        PreferenceId = item,
                        CustomerId = customer.Id
                    });
                }
            }

            var createdCustomerId = await _customerRepository.CreateAsync(customer);

            return Ok($"Customer with {createdCustomerId} Id was successfuly created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCustomersAsync(Guid id, CreateOrEditCustomerRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request is empty");
            }

            var exsistingCustomer = await _customerRepository.GetCustomerForUpdate(id);

            if (exsistingCustomer is null)
            {
                return NotFound("Customer wasn't found");
            }

            if (request.PreferenceIds.Count > 0)
            {
                foreach (var item in request.PreferenceIds)
                {
                    if (!exsistingCustomer.CustomerPreferences.Any(c => c.PreferenceId == item))
                    {
                        exsistingCustomer.CustomerPreferences.Add(new CustomerPreference
                        {
                            PreferenceId = item,
                            CustomerId = exsistingCustomer.Id
                        });
                    }
                }
            }

            var updatedCustomer = _mapper.Map(request, exsistingCustomer);

            var k = await _customerRepository.UpdateAsync(updatedCustomer);

            return Ok(_mapper.Map<CustomerShortResponse>(updatedCustomer));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await _customerRepository.RemoveAsync(id);

            return Ok("Customer was successfully removed");
        }
    }
}