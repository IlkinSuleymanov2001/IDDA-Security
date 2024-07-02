using Goverment.AuthApi.Commans.Utility.Caches;
using Microsoft.Extensions.Caching.Memory;

namespace Goverment.AuthApi.Commans.Utility.Caches.InMemory
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T GetData<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public bool KeyExists(string key)
        {
            return _memoryCache.TryGetValue(key, out var _);
        }

        public object RemoveData(string key)
        {
            if (_memoryCache.TryGetValue(key, out var _))
            {
                _memoryCache.Remove(key);
                return true;
            }
            return false;

        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            _memoryCache.Set(key, value, expirationTime);
            return true;
        }
    }
}
