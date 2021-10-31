using System;
using MongoDB.Driver;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;
using Otus.Teaching.Pcf.Administration.MongoDataAccess.Helpers;

namespace Otus.Teaching.Pcf.Administration.MongoDataAccess.Data
{
    public class MongoDbInitializer : IDbInitializer
    {
        private readonly IMongoDatabase _database;

        public MongoDbInitializer(IMongoDbOptions options)
        {
            var client = new MongoClient(options.ConnectionString);
            _database = client.GetDatabase(options.DatabaseName);
        }

        public void InitializeDb()
        {
            var collectionName = MongoCollectionHelper.GetCollectionName(typeof(Employee));
            if (collectionName is null)
                throw new InvalidOperationException(
                    $"The class {nameof(Employee)} does not have BsonCollectionAttribute attribute");
            _database.DropCollection(collectionName);
            var employees =
                _database.GetCollection<Employee>(collectionName);
            if (employees != null && employees.CountDocuments(FilterDefinition<Employee>.Empty) == 0)
            {
                employees.InsertMany(FakeDataFactory.Employees);
            }
            
            var roleCollectionName = MongoCollectionHelper.GetCollectionName(typeof(Role));
            if (roleCollectionName is null)
                throw new InvalidOperationException(
                    $"The class {nameof(Role)} does not have BsonCollectionAttribute attribute");

            _database.DropCollection(roleCollectionName);
            var roles = _database.GetCollection<Role>(roleCollectionName);
            if (roles != null && roles.CountDocuments(FilterDefinition<Role>.Empty) == 0)
            {
                roles.InsertMany(FakeDataFactory.Roles);
            }
        }
    }
}