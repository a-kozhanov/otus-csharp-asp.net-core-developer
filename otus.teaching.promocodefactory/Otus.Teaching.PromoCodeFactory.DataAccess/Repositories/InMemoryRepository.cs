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
        private List<T> Data { get; set; }

        public InMemoryRepository(List<T> data)
        {
            Data = new List<T>(data);
        }

        public Task<List<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task CreateAsync(T employee)
        {
            Data.Add(employee);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T employee)
        {
            Data.RemoveAll(x => x.Id == employee.Id);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T employee)
        {
            if (Data.All(x => x.Id != employee.Id))
                throw new Exception($"Employee with Id = {employee.Id} not found");

            DeleteAsync(employee);
            CreateAsync(employee);
            return Task.CompletedTask;
        }
    }
}