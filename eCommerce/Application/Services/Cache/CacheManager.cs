
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace eCommerce.Application.Services.Cache
{
    // Cache yönetim sınıfı
    public class CacheManager : ICacheService
    {
        // Dağıtık cache bağımlılığı
        private readonly IDistributedCache _cache;

        // Yapılandırıcı, bağımlılık olarak dağıtık cache'i alır
        public CacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        // Cache'den veri alır
        public async Task<T> GetAsync<T>(string key)
        {
            var serializedValue = await _cache.GetStringAsync(key);
            if (serializedValue == null) return default;

            return JsonSerializer.Deserialize<T>(serializedValue);
        }

        // Cache'den veri siler
        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        // Cache'e veri ekler
        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };

            var serializedValue = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, serializedValue, options);

        }
    }
}
