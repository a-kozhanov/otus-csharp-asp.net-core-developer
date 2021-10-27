using Microsoft.EntityFrameworkCore;
using Otus.Teaching.PromoCodeFactory.Core.Abstractions.Repositories;
using Otus.Teaching.PromoCodeFactory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected DbContext Context { get; }
        protected DbSet<T> DbSet { get; }

        protected EfRepository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        public async Task<Guid> CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);

            await SaveChanges();

            return entity.Id;
        }

        public async Task CreateBulkAsync(ICollection<T> entities)
        {
            await DbSet.AddRangeAsync(entities);

            await SaveChanges();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task RemoveAsync(Guid id)
        {
            var item = await DbSet.FirstOrDefaultAsync(e => e.Id == id);
            DbSet.Remove(item);

            await SaveChanges();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await SaveChanges();

            return await DbSet.FirstOrDefaultAsync(e => e.Id == entity.Id);
        }

        private async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }
    }
}