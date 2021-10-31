using System;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;

namespace Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Helpers
{
    public static class MongoCollectionHelper
    {
        public static string GetCollectionName(Type entityType)
        {
            if (entityType == typeof(Customer))
            {
                return "Customers";
            }

            if (entityType == typeof(Preference))
            {
                return "Preferences";
            }

            if (entityType == typeof(PromoCode))
            {
                return "Promocodes";
            }

            throw new ArgumentException();
        }
    }
}