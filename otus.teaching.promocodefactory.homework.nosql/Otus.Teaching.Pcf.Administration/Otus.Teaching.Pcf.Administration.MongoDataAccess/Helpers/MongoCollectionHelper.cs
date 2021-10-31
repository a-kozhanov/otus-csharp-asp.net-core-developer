using System.Linq;
using System.Reflection;
using Otus.Teaching.Pcf.Administration.Core.Attributes;

namespace Otus.Teaching.Pcf.Administration.MongoDataAccess.Helpers
{
    public class MongoCollectionHelper
    {
        public static string GetCollectionName(ICustomAttributeProvider entityType)
        {
            return ((BsonCollectionAttribute) entityType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
    }
}