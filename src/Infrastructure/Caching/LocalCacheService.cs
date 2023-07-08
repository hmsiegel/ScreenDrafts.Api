namespace ScreenDrafts.Api.Infrastructure.Caching;
public sealed class LocalCacheService : ICacheService
{
    private readonly ILogger<LocalCacheService> _logger;
    private readonly IMemoryCache _cache;

    public LocalCacheService(
        ILogger<LocalCacheService> logger,
        IMemoryCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    public T? Get<T>(string key)
    {
        return _cache.Get<T>(key);
    }

    public Task<T?> GetAsync<T>(string key, CancellationToken token = default)
    {
        return Task.FromResult(Get<T>(key));
    }

    public void Refresh(string key)
    {
        _cache.TryGetValue(key, out _);
    }

    public Task RefreshAsync(string key, CancellationToken token = default)
    {
        Refresh(key);
        return Task.CompletedTask;
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }

    public Task RemoveAsync(string key, CancellationToken token = default)
    {
        Remove(key);
        return Task.CompletedTask;
    }

    public void Set<T>(string key, T value, TimeSpan? slidingExpiration = null)
    {
        slidingExpiration ??= TimeSpan.FromMinutes(10);
        _cache.Set(key, value, new MemoryCacheEntryOptions { SlidingExpiration = slidingExpiration });
        _logger.LogDebug("Added to cache: {key}", key);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? slidingExpiration = null, CancellationToken cancellationToken = default)
    {
        Set(key, value, slidingExpiration);
        return Task.CompletedTask;
    }
}
