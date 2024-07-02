using Goverment.AuthApi.Commans.Utility.Caches;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Goverment.AuthApi.Commans.Utility.Caches.Redis
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _db;
        private readonly IConfiguration _configuration;
        private readonly RedisConfig _redisConfig;
        public CacheService(IConfiguration configuration)
        {

            _configuration = configuration;
            _redisConfig = _configuration.GetSection("RedisConfig").Get<RedisConfig>();
            _db = ConnectionMultiplexer.Connect(_redisConfig.ServerAndPort).GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
                return JsonConvert.DeserializeObject<T>(value);

            return default;
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }
        public object RemoveData(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist)
                return _db.KeyDelete(key);

            return false;
        }
        public bool KeyExists(string key)
        {
            return _db.KeyExists(key);
        }
    }
}
