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
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController : ControllerBase
    {
        private readonly IPromoCodeRepository _promoCodeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public PromocodesController(IPromoCodeRepository promoCodeRepository, ICustomerRepository customerRepository,
            IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _promoCodeRepository = promoCodeRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync()
        {
            //TODO: Получить все промокоды 
            var promoCodes = await _promoCodeRepository.GetAllAsync();

            return Ok(_mapper.Map<List<PromoCodeShortResponse>>(promoCodes));
        }

        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {
            if (request is null)
            {
                return BadRequest("Request is empty");
            }

            var customers = await _customerRepository.GetAllAsync(c => c.CustomerPreferences.Any(p => p.Preference.Name == request.Preference));

            var employee = await _employeeRepository.GetEmployeeAsync(e => (e.FirstName + " " + e.LastName) == request.PartnerName);

            if (customers.Count > 0)
            {
                var promocodes = new List<PromoCode>();
                foreach (var customer in customers)
                {
                    var promoCode = _mapper.Map<PromoCode>(request);
                    promoCode.Id = Guid.NewGuid();
                    promoCode.CustomerId = customer.Id;
                    promoCode.PreferenceId = customer.CustomerPreferences.FirstOrDefault().PreferenceId;
                    promoCode.PartnerManagerId = employee.Id;
                    promocodes.Add(promoCode);
                }

                await _promoCodeRepository.CreateBulkAsync(promocodes);
            }
            else
            {
                var promoCode = _mapper.Map<PromoCode>(request);
                await _promoCodeRepository.CreateAsync(promoCode);
            }

            return Ok();
        }
    }
}