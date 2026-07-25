namespace DomainLayer.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string CacheKey);
        Task SetAsync(string CacheKey, string Value, TimeSpan TimeToLive);
    }
}
