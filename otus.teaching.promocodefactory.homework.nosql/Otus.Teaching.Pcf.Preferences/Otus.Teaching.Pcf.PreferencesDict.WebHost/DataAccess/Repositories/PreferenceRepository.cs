using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Otus.Teaching.Pcf.PreferencesDict.WebHost.Core.Abstraction;
using Otus.Teaching.Pcf.PreferencesDict.WebHost.Core.Domain;

namespace Otus.Teaching.Pcf.PreferencesDict.WebHost.DataAccess.Repositories
{
    public class PreferenceRepository : IRepository<Preference>
    {
        private readonly IDistributedCache _cache;

        public PreferenceRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<Preference>> GetAllAsync()
        {
            var content = await _cache.GetStringAsync(nameof(Preference));
            var items = JsonSerializer.Deserialize<IEnumerable<Preference>>(content);
            return items;
        }

        public async Task<Preference> GetByIdAsync(Guid id)
        {
            var content = await _cache.GetStringAsync(nameof(Preference));
            var items = JsonSerializer.Deserialize<IEnumerable<Preference>>(content);
            return items.FirstOrDefault(i => i.Id == id);
        }
    }
}