using Application.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class CachingService<T> : ICachingService<T> where T : class
    {
        private readonly IDistributedCache _cache;

        public CachingService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<T>> GetData(string key)
        {
            byte[]? data = await _cache.GetAsync(key);
            IEnumerable<T>? list = null;
            if (data != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(data);
                list = JsonSerializer.Deserialize<IEnumerable<T>>(cachedDataString);
            }
            return list;
        }

        public async Task SetData(string key, byte[] data, DistributedCacheEntryOptions options)
        {
            await _cache.SetAsync(key, data, options);
        }
    }
}