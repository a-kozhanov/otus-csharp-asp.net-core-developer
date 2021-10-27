using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected ICollection<T> Data { get; set; }

        public InMemoryRepository(ICollection<T> data)
        {
            Data = data;
        }

        public Task<ICollection<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<Guid> CreateAsync(T entity)
        {
            Data.Add(entity);

            return Task.FromResult(entity.Id);
        }

        public Task CreateBulkAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                Data.Add(entity);
            }

            return Task.FromResult(Data);
        }

        public Task<T> UpdateAsync(T entity)
        {
            var previousEntity = Data.FirstOrDefault(d => d.Id == entity.Id);
            Data.Remove(previousEntity);
            Data.Add(entity);

            return Task.FromResult(Data.FirstOrDefault(e => e.Id == entity.Id));
        }

        public Task RemoveAsync(Guid id)
        {
            var entityForDelete = Data.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(Data.Remove(entityForDelete));
        }
    }
}