using System;

namespace Otus.Teaching.PromoCodeFactory.Core.Domain
{
    public interface ICurrentDateTimeProvider
    {
        DateTime CurrentDateTime { get; }
    }
}