using System;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Domain;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Dto;
using Otus.Teaching.Pcf.ReceivingFromPartner.WebHost.Models;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.WebHost.Mappers
{
    public class PromoCodeMapper
    {
        public static PromoCode MapFromModel(ReceivingPromoCodeRequest request, PreferenceDto preference,
            Partner partner)
        {
            var promocode = new PromoCode
            {
                PartnerId = partner.Id,
                Partner = partner,
                Code = request.PromoCode,
                ServiceInfo = request.ServiceInfo,
                BeginDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(30),
                PreferenceId = preference.Id,
                PartnerManagerId = request.PartnerManagerId
            };

            return promocode;
        }
    }
}