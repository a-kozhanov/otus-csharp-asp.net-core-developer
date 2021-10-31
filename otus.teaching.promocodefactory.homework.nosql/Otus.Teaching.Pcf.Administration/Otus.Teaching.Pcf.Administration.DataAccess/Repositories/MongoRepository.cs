using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using Otus.Teaching.Pcf.Administration.Core.Abstractions.Repositories;
using Otus.Teaching.Pcf.Administration.Core.Domain;

namespace Otus.Teaching.Pcf.Administration.DataAccess.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _mongoCollection;

        public MongoRepository(IMongoCollection<T> mongoCollection)
        {
            _mongoCollection = mongoCollection;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _mongoCollection.FindAsync(FilterDefinition<T>.Empty);
            return await entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _mongoCollection.FindAsync(e => e.Id == id);
            return await entity.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
        {
            var range = await _mongoCollection.FindAsync(e => ids.Contains(e.Id));
            return await range.ToListAsync();
        }

        public async Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate)
        {
            var entity = await _mongoCollection.FindAsync(predicate);
            return await entity.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            var entities = await _mongoCollection.FindAsync(predicate);
            return await entities.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _mongoCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _mongoCollection.ReplaceOneAsync(item => item.Id == entity.Id, entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await _mongoCollection.DeleteOneAsync(item => item.Id == entity.Id);
        }
    }
}