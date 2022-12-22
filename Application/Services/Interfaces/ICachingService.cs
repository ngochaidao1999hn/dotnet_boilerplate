using Microsoft.Extensions.Caching.Distributed;

namespace Application.Services.Interfaces
{
    public interface ICachingService<T> where T : class
    {
        public Task<IList<T>> GetData(string key);

        public Task SetData(string key, byte[] data, DistributedCacheEntryOptions options = null);
    }
}