using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.PromoCodeFactory.Core.Domain;

namespace Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(T entity);
        Task CreateBulkAsync(ICollection<T> entities);
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(Guid id);
    }
}