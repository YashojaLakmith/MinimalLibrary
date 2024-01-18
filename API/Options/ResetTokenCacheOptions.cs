using Microsoft.Extensions.Caching.Distributed;

namespace API.Options
{
    public class ResetTokenCacheOptions : DistributedCacheEntryOptions
    {
        public ResetTokenCacheOptions()
        {
            SlidingExpiration = TimeSpan.FromMinutes(5);
        }
    }
}
