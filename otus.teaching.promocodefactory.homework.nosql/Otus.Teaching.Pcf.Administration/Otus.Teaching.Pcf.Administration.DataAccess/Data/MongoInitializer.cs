using MongoDB.Driver;
using Otus.Teaching.Pcf.Administration.Core.Domain.Administration;

namespace Otus.Teaching.Pcf.Administration.DataAccess.Data
{
    public class MongoInitializer:IDbInitializer
    {
        private readonly IMongoCollection<Employee> _employeesCollection;
        private readonly IMongoCollection<Role> _rolesCollection;

        public MongoInitializer(
            IMongoCollection<Employee> employeesCollection,
            IMongoCollection<Role> rolesCollection)
        {
            _employeesCollection = employeesCollection;
            _rolesCollection = rolesCollection;
        }
        
        public void InitializeDb()
        {
            if (_employeesCollection.CountDocuments(FilterDefinition<Employee>.Empty) == 0)
            {
                _employeesCollection.InsertMany(FakeDataFactory.Employees);
            }
            
            if (_rolesCollection.CountDocuments(FilterDefinition<Role>.Empty) == 0)
            {
                _rolesCollection.InsertMany(FakeDataFactory.Roles);
            }
        }
    }
}