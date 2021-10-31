using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Gateways;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.Controllers
{
    /// <summary>
    /// Промокоды
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PromocodesController
        : ControllerBase
    {
        private readonly IRepository<PromoCode> _promoCodesRepository;
        //private readonly IRepository<Preference> _preferencesRepository;
        private readonly IRepository<Customer> _customersRepository;

        private readonly IPreferencesGateway _preferencesGateway;

        public PromocodesController(
            IRepository<PromoCode> promoCodesRepository, 
             IRepository<Customer> customersRepository,
            IPreferencesGateway preferencesGateway)
        {
            _promoCodesRepository = promoCodesRepository;
            _customersRepository = customersRepository;
            _preferencesGateway = preferencesGateway;
        }
        
        /// <summary>
        /// Получить все промокоды
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PromoCodeShortResponse>>> GetPromocodesAsync()
        {
            var promocodes = await _promoCodesRepository.GetAllAsync();

            var response = promocodes.Select(x => new PromoCodeShortResponse()
            {
                Id = x.Id,
                Code = x.Code,
                BeginDate = x.BeginDate.ToString("yyyy-MM-dd"),
                EndDate = x.EndDate.ToString("yyyy-MM-dd"),
                PartnerId = x.PartnerId,
                ServiceInfo = x.ServiceInfo
            }).ToList();

            return Ok(response);
        }
        
        /// <summary>
        /// Создать промокод и выдать его клиентам с указанным предпочтением
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GivePromoCodesToCustomersWithPreferenceAsync(GivePromoCodeRequest request)
        {
            //Получаем предпочтение по имени
            var preference =
                (await _preferencesGateway.GetPreferencesAsync()).FirstOrDefault(
                    item => item.Id == request.PreferenceId);

            if (preference == null)
            {
                return BadRequest();
            }

            //  Получаем клиентов с этим предпочтением:
            var customers = await _customersRepository
                .GetWhere(d => d.PreferencesId.Any(x =>
                    x== preference.Id));

            PromoCode promoCode = PromoCodeMapper.MapFromModel(request, preference);
            
            //Добавляем промокод клинетам 
            foreach (var customer in customers)
            {
                customer.PromoCodesId.Add(promoCode.Id);
                await _customersRepository.UpdateAsync(customer);
            }
            await _promoCodesRepository.AddAsync(promoCode);

            return CreatedAtAction(nameof(GetPromocodesAsync), new { }, null);
        }
    }
}