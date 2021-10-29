using System;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain
{
    public class CurrentDateTimeProvider
        : ICurrentDateTimeProvider
    {
        public DateTime CurrentDateTime => DateTime.Now;
    }
}