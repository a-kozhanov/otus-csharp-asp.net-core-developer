using System;
using System.Collections.Generic;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.UnitTests.Builders
{
    public static class PartnerBuilder
    {
        public static Partner CreateBasePartner()
        {
            return new Partner()
            {
                Id = Guid.Parse("7d994823-8226-4273-b063-1a95f3cc1df8"),
                Name = "Суперигрушки",
                IsActive = true,
                PartnerLimits = new List<PartnerPromoCodeLimit>()
                {
                    new PartnerPromoCodeLimit()
                    {
                        Id = Guid.Parse("e00633a5-978a-420e-a7d6-3e1dab116393"),
                        CreateDate = new DateTime(2020, 07, 9),
                        EndDate = new DateTime(2020, 10, 9),
                        Limit = 100
                    }
                }
            };
        }

        public static Partner WithActiveLimit(this Partner partner)
        {
            partner.PartnerLimits = new List<PartnerPromoCodeLimit>();
            partner.PartnerLimits.Add(new PartnerPromoCodeLimit()
            {
                Id = Guid.Parse("c9bef066-3c5a-4e5d-9cff-bd54479f075e"),
                CreateDate = new DateTime(2020, 07, 9),
                EndDate = new DateTime(2020, 10, 9),
                Limit = 100
            });
            partner.NumberIssuedPromoCodes = 20;

            return partner;
        }

        public static Partner ResetNumberIssuedPromoCodes(this Partner partner)
        {         
            partner.NumberIssuedPromoCodes = 0;

            return partner;
        }

        public static Partner WithNotActiveLimit(this Partner partner)
        {
            partner.PartnerLimits = new List<PartnerPromoCodeLimit>();
            partner.PartnerLimits.Add(new PartnerPromoCodeLimit()
            {
                Id = Guid.Parse("c9bef066-3c5a-4e5d-9cff-bd54479f075e"),
                CreateDate = new DateTime(2020, 07, 9),
                EndDate = new DateTime(2020, 10, 9),
                CancelDate = new DateTime(2020, 8, 9),
                Limit = 100
            });

            return partner;
        }

        public static Partner WithNegativeLimit(this Partner partner)
        {
            partner.PartnerLimits = new List<PartnerPromoCodeLimit>();
            partner.PartnerLimits.Add(new PartnerPromoCodeLimit()
            {
                Id = Guid.Parse("c9bef066-3c5a-4e5d-9cff-bd54479f075e"),
                CreateDate = new DateTime(2020, 07, 9),
                EndDate = new DateTime(2020, 10, 9),
                CancelDate = new DateTime(2020, 8, 9),
                Limit = -10
            });

            return partner;
        }
    }
}