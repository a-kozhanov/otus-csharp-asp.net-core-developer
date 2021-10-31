using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.PreferencesDict.WebHost.Core.Domain;

namespace Otus.Teaching.Pcf.PreferencesDict.WebHost.Core.Abstraction
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);
    }
}