using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class PromoCodeRepository : EfRepository<PromoCode>, IPromoCodeRepository
    {
        public PromoCodeRepository(DataContext context) : base(context)
        {
        }
    }
}