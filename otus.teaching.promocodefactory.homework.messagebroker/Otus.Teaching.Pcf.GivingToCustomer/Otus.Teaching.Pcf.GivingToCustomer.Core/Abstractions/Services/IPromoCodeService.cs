using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services
{
    public interface IPromoCodeService
    {
        Task<IEnumerable<PromoCode>> GetPromoCodesAsync();

        Task GivePromoCodesToCustomersWithPreferenceAsync(PromoCode promoCode);
    }
}