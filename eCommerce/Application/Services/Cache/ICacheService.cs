namespace eCommerce.Application.Services.Cache
{
    // Cache servisi arayüzü
    public interface ICacheService
    {
        // Cache'e veri ekler
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        // Cache'den veri alır
        Task<T> GetAsync<T>(string key);
        // Cache'den veri siler
        Task RemoveAsync(string key);
    }
}
