using Microsoft.Extensions.Caching.Distributed;

namespace API.Options
{
    public class SessionCacheOptions : DistributedCacheEntryOptions
    {
        public SessionCacheOptions()
        {
            SlidingExpiration = TimeSpan.FromMinutes(30);
        }
    }
}
