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
        public async Task<IList<T>> GetData(string key)
        {
            byte[] data = await _cache.GetAsync(key);
            IList<T> list = null;
            if (data != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(data);
                list = JsonSerializer.Deserialize<IList<T>>(cachedDataString);
            }
            return list;
        }

        public async Task SetData(string key, byte[] data, DistributedCacheEntryOptions options = null)
        {
            await _cache.SetAsync(key, data, options);
        }
    }
}
