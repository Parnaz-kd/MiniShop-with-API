namespace MiniShop.Api.Application.Services.Interfaces
{
    public interface ICacheService
    {
        T GetOrSet<T>(string key, Func<T> factory, TimeSpan ttl);
    }
}
