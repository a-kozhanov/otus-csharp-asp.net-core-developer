using System;
using System.Threading.Tasks;
using MassTransit;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;
using Otus.Teaching.Pcf.Integration.Contracts;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Application.Integration.Consumers
{
    public class PromoCodeCreatedEventConsumer:IConsumer<PromoCodeCreatedEvent>
    {
        private readonly IPromoCodeService _promoCodeService;

        public PromoCodeCreatedEventConsumer(IPromoCodeService promoCodeService)
        {
            _promoCodeService = promoCodeService;
        }

        public async Task Consume(ConsumeContext<PromoCodeCreatedEvent> context)
        {
            var message = context.Message;
            var promoCode = new PromoCode()
            {
                Id = message.PromoCodeId,
                PartnerId = message.PartnerId,
                Code = message.PromoCode,
                ServiceInfo = message.ServiceInfo,
                BeginDate = DateTime.Parse(message.BeginDate),
                EndDate = DateTime.Parse(message.EndDate),
                PreferenceId = message.PreferenceId
            };
            await _promoCodeService.GivePromoCodesToCustomersWithPreferenceAsync(promoCode);
        }
    }
}