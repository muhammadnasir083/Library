using Library.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Library.Services
{
    public interface ICachingService
    {
        T Get<T>(string key) where T : class;
        void Set<T>(string key, T value) where T : class;
    }
    public class CachingService : ICachingService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IOptionsMonitor<LibrarySettings> _librarySettings;
        public CachingService(IMemoryCache memoryCache
            , IOptionsMonitor<LibrarySettings> librarySettings)
        {
            _memoryCache = memoryCache; 
            _librarySettings = librarySettings; 
        }


        public T Get<T>(string key) where T : class
            => _memoryCache.Get<T>(key);

        public void Set<T>(string key, T value) where T : class
            => _memoryCache.Set(key, value, new MemoryCacheEntryOptions 
            { SlidingExpiration = TimeSpan.FromDays(_librarySettings.CurrentValue.CacheExpirationDays) });
    }
}
