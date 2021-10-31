using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Otus.Teaching.Pcf.PreferencesDict.WebHost.Core.Domain;

namespace Otus.Teaching.Pcf.PreferencesDict.WebHost.DataAccess.Data
{
    public class RedisInitializer : IDbInitializer
    {
        private readonly IDistributedCache _cache;

        public RedisInitializer(IDistributedCache cache)
        {
            _cache = cache;
        }

        public void InitializeDb()
        {
            if (string.IsNullOrEmpty(_cache.GetString(nameof(Preference))))
            {
                var content = JsonSerializer.Serialize(DataFactory.Preferences);
                _cache.SetString(nameof(Preference), content);
            }
        }
    }
}