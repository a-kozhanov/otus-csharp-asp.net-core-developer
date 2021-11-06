using System;

namespace Otus.Teaching.Pcf.GivingToCustomer.Integration.Dto
{
    public class GivePromoCodeToCustomerDto
    {
        public string ServiceInfo { get; set; }

        public Guid PartnerId { get; set; }

        public Guid PromoCodeId { get; set; }
        
        public string PromoCode { get; set; }

        public Guid PreferenceId { get; set; }

        public string BeginDate { get; set; }

        public string EndDate { get; set; }
        
        public Guid? PartnerManagerId { get; set; }
    }
}