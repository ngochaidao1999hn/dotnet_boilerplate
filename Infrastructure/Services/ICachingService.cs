using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ICachingService<T> where T : class
    {
        public Task<IList<T>> GetData(string key);

        public Task SetData(string key, byte[] data, DistributedCacheEntryOptions options = null);
    }
}
