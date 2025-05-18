//using System;

using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace GitHubPortfolio.Service
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public InMemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> factory, TimeSpan expiration)
        {
            if (_cache.TryGetValue(key, out T result))
                return result;

            result = await factory();
            _cache.Set(key, result, expiration);
            return result;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
