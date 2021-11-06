using System.Text.Json;

using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Domain;
using Otus.Teaching.Pcf.ReceivingFromPartner.Integration.Dto;
using Otus.Teaching.Pcf.ReceivingFromPartner.QueueLibrary;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Integration
{
    public class GivingPromoCodeToCustomerNotifier
    {
        private readonly QueueSender _queueSender;

        public GivingPromoCodeToCustomerNotifier(QueueSender queueSender)
        {
            _queueSender = queueSender;
        }

        public void GivePromoCodeToCustomer(PromoCode promoCode)
        {
            var dto = new GivePromoCodeToCustomerDto()
            {
                PartnerId = promoCode.Partner.Id,
                BeginDate = promoCode.BeginDate.ToShortDateString(),
                EndDate = promoCode.EndDate.ToShortDateString(),
                PreferenceId = promoCode.PreferenceId,
                PromoCode = promoCode.Code,
                ServiceInfo = promoCode.ServiceInfo,
                PartnerManagerId = promoCode.PartnerManagerId
            };
            var message = JsonSerializer.Serialize(dto, typeof(GivePromoCodeToCustomerDto));

            _queueSender.Send(message, "GivinigPromoCodeToCustomerPromoCode");
        }
    }
}