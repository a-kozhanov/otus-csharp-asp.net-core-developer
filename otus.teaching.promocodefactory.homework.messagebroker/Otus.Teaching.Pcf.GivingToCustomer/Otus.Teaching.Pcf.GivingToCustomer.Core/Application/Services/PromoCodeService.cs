using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Application.Exceptions;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Application.Services
{
    public class PromoCodeService:IPromoCodeService
    {
        
        private readonly IRepository<PromoCode> _promoCodesRepository;
        private readonly IRepository<Preference> _preferencesRepository;
        private readonly IRepository<Customer> _customersRepository;

        public PromoCodeService(
            IRepository<PromoCode> promoCodesRepository, 
            IRepository<Preference> preferencesRepository, 
            IRepository<Customer> customersRepository)
        {
            _promoCodesRepository = promoCodesRepository;
            _preferencesRepository = preferencesRepository;
            _customersRepository = customersRepository;
        }
        
        public async Task<IEnumerable<PromoCode>> GetPromoCodesAsync()
        {
            var promoCodes = await _promoCodesRepository.GetAllAsync();
            return promoCodes;
        }

        public async Task GivePromoCodesToCustomersWithPreferenceAsync(PromoCode promoCode)
        {
            var preference = await _preferencesRepository.GetByIdAsync(promoCode.PreferenceId);

            if (preference == null)
            {
                throw new EntityNotFoundException(promoCode.PreferenceId);
            }

            //  Получаем клиентов с этим предпочтением:
            var customers = await _customersRepository
                .GetWhere(d => d.Preferences.Any(x =>
                    x.Preference.Id == preference.Id));

            promoCode.Preference = preference;
            promoCode.PreferenceId = preference.Id;

            promoCode.Customers = new List<PromoCodeCustomer>();

            foreach (var item in customers)
            {
                promoCode.Customers.Add(new PromoCodeCustomer()
                {

                    CustomerId = item.Id,
                    Customer = item,
                    PromoCodeId = promoCode.Id,
                    PromoCode = promoCode
                });
            };
            
            await _promoCodesRepository.AddAsync(promoCode);
        }
    }
}