using DomainLayer.Contracts;
using ServiceAbstraction;
using System.Text.Json;

namespace Service
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string CacheKey)
        {
            return await cacheRepository.GetAsync(CacheKey);
        }

        public async Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive)
        {
            var Value = JsonSerializer.Serialize(CacheValue);
            await cacheRepository.SetAsync(CacheKey, Value, TimeToLive);
        }
    }
}
