using MiniShop.Api.Application.Services.Interfaces;

namespace MiniShop.Api.Application.Services.Implementations
{
    public sealed class CacheService : ICacheService
    {
        private readonly Dictionary<string, (DateTime expires, object value)> _store = new();
        private readonly object _lock = new();

        public T GetOrSet<T>(string key, Func<T> factory, TimeSpan ttl)
        {
            lock (_lock)
            {
                if (_store.TryGetValue(key, out var entry) && entry.expires > DateTime.Now)
                    return (T)entry.value;

                var value = factory();
                _store[key] = (DateTime.Now.Add(ttl), value!);
                return value;
            }
        }
    }
}
