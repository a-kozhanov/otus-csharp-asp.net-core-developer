using System.Collections.Generic;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Preference : BaseEntity
    {
        public string Name { get; set; }
        public IList<PromoCode> PromoCodes { get; set; }
        public virtual ICollection<CustomerPreference> CustomerPreferences { get; set; }
    }
}