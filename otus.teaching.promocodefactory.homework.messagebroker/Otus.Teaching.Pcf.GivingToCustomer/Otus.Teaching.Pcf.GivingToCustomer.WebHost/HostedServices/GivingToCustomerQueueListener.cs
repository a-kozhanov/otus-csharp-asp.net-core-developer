using Microsoft.Extensions.DependencyInjection;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.GivingToCustomer.Integration.Dto;
using Otus.Teaching.Pcf.GivingToCustomer.QueueLibrary;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Mappers;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.HostedServices
{
    public class GivingToCustomerQueueListener : QueueListener
    {
        private readonly IServiceProvider _serviceProvider;

        public GivingToCustomerQueueListener(IServiceProvider serviceProvider, BrokerSettings brokerSettings, 
            ReceiverSettings receiverSettings) : base(brokerSettings, receiverSettings)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ProcessMessageAsync(string message)
        {
            using var scope = _serviceProvider.CreateScope();
            var promoCodesRepository = scope.ServiceProvider.GetService<IRepository<PromoCode>>();
            var preferencesRepository = scope.ServiceProvider.GetService<IRepository<Preference>>();
            var customersRepository = scope.ServiceProvider.GetService<IRepository<Customer>>();
            var dto = JsonSerializer.Deserialize<GivePromoCodeToCustomerDto>(message);
            var preference = await preferencesRepository.GetByIdAsync(dto.PreferenceId);

            if (preference == null)
            {
                throw new InvalidOperationException($"Preference with id {dto.PreferenceId} cannot be processed");
            }

            //  Получаем клиентов с этим предпочтением:
            var customers = await customersRepository
                .GetWhere(d => d.Preferences.Any(x =>
                    x.Preference.Id == preference.Id));

            var request = new GivePromoCodeRequest
            {
                PreferenceId = dto.PreferenceId, 
                PromoCode = dto.PromoCode, 
                BeginDate = dto.BeginDate, 
                EndDate = dto.EndDate, 
                PartnerId = dto.PartnerId, 
                ServiceInfo = dto.ServiceInfo, 
                PromoCodeId = dto.PromoCodeId
            };

            var promoCode = PromoCodeMapper.MapFromModel(request, preference, customers);

            await promoCodesRepository.AddAsync(promoCode);
        }
    }
}
