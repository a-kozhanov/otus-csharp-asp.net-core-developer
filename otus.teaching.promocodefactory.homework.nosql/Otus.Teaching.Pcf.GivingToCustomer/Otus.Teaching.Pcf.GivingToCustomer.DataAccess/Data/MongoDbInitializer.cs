using MongoDB.Driver;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Domain;

namespace Otus.Teaching.Pcf.GivingToCustomer.DataAccess.Data
{
    public class MongoDbInitializer:IDbInitializer
    {
        private readonly IMongoCollection<Customer> _customersMongoCollection;
        private readonly IMongoCollection<Preference> _preferencesMongoCollection;
        private readonly IMongoCollection<PromoCode> _promoCodesMongoCollection;

        public MongoDbInitializer(
            IMongoCollection<Customer> customersMongoCollection, 
            IMongoCollection<Preference> preferencesMongoCollection, 
            IMongoCollection<PromoCode> promoCodesMongoCollection)
        {
            _customersMongoCollection = customersMongoCollection;
            _preferencesMongoCollection = preferencesMongoCollection;
            _promoCodesMongoCollection = promoCodesMongoCollection;
        }

        public void InitializeDb()
        {
            if (_customersMongoCollection.CountDocuments(FilterDefinition<Customer>.Empty) == 0)
            {
                _customersMongoCollection.InsertMany(FakeDataFactory.Customers);
            }
            
            if (_preferencesMongoCollection.CountDocuments(FilterDefinition<Preference>.Empty) == 0)
            {
                _preferencesMongoCollection.InsertMany(FakeDataFactory.Preferences);
            }
            
            // if (_promoCodesMongoCollection.CountDocuments(FilterDefinition<PromoCode>.Empty) == 0)
            // {
            //     _promoCodesMongoCollection.InsertMany(FakeDataFactory.PromoCodes);
            // }
        }
    }
}