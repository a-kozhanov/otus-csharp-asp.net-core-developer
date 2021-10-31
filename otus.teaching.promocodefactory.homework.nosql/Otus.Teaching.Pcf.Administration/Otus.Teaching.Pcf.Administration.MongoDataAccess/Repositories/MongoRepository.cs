using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain;
using Otus.Teaching.Pcf.Administration.MongoDataAccess.Helpers;

namespace Otus.Teaching.Pcf.Administration.MongoDataAccess.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDbOptions options)
        {
            var client = new MongoClient(options.ConnectionString);
            var database = client.GetDatabase(options.DatabaseName);
            _collection = database.GetCollection<T>(MongoCollectionHelper.GetCollectionName(typeof(T)));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var res = await _collection.FindAsync(FilterDefinition<T>.Empty);
            return res.ToEnumerable();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var res = await _collection.FindAsync(entity => entity.Id == id);
            return await res.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
        {
            var res = await _collection.FindAsync(entity => ids.Contains(entity.Id));
            return res.ToEnumerable();
        }

        public async Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate)
        {
            var res = await _collection.FindAsync(predicate);
            return await res.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            var res = await _collection.FindAsync(predicate);
            return await res.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await _collection.DeleteOneAsync(e => e.Id == entity.Id);
        }
    }
}