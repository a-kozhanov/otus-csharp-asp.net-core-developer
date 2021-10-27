using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class PreferenceRepository : EfRepository<Preference>, IPreferenceRepository
    {
        public PreferenceRepository(DataContext context) : base(context)
        {
        }
    }
}