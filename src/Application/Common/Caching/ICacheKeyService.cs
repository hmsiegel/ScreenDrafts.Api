namespace ScreenDrafts.Api.Application.Common.Caching;
public interface ICacheKeyService : IScopedService
{
    string GetCacheKey(string name, object id);
}
