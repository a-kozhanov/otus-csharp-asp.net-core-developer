using System;
using System.Collections.Generic;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Dto;

namespace Otus.Teaching.Pcf.GivingToCustomer.Core.Domain
{
    public class PromoCode
        : BaseEntity
    {
        public string Code { get; set; }

        public string ServiceInfo { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid PartnerId { get; set; }
        
        public PreferenceDto Preference { get; set; }
        
        //public virtual ICollection<PromoCodeCustomer> Customers { get; set; }
    }
}