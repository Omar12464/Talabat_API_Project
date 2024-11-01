using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat_Core.Repositories_InterFaces
{
    public interface IResponseCacheService
    {
        //CacheDate 
        Task CacheResponseAsync(string cacheKey,object response,TimeSpan ExpiredTime);
        //Get Cached Data
        Task<string?> GetCachedResponse(string CachKey);
        //
    }
}
