namespace ServiceAbstraction
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string CacheKey);
        Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive);
    }
}
