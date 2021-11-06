// ReSharper disable once CheckNamespace
// ReSharper disable once IdentifierTypo

using System;

namespace Otus.Teaching.Pcf.Integration.Contracts
{
    // ReSharper disable once InconsistentNaming
    public interface PromoCodeCreatedEvent
    {
        string ServiceInfo { get; set; }

        Guid PartnerId { get; set; }

        Guid PromoCodeId { get; set; }
        
        string PromoCode { get; set; }

        Guid PreferenceId { get; set; }

        string BeginDate { get; set; }

        string EndDate { get; set; }
        
        Guid? PartnerManagerId { get; set; }
    }
}