using MongoDB.Bson.Serialization.Conventions;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;

namespace Otus.Teaching.Pcf.GivingToCustomer.DataAccess
{
    public static class MongoConventionsConfigurator
    {
        public static void CreateDefault()
        {
            var pack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
            };

            ConventionRegistry.Register(
                "CamelCase Convention",
                pack,
                t => t.FullName!.StartsWith(typeof(BaseEntity).Namespace!));
        }
    }
}