using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat_Core.Repositories_InterFaces;

namespace Talabat_Service
{
    public class ResponseCachedService : IResponseCacheService
    {
        private readonly IDatabase _database;

        public ResponseCachedService(IConnectionMultiplexer Redis
        {
            _database = Redis.GetDatabase();

        }
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan ExpiredTime)
        {
            if (response is null) return;
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var serializedResponse=JsonSerializer.Serialize(response,options);
            await _database.StringSetAsync(cacheKey,serializedResponse,ExpiredTime);
        }

        public async Task<string?> GetCachedResponse(string CachKey)
        {
            var cachResponse = await _database.StringGetAsync(CachKey);

            if (cachResponse.IsNullOrEmpty) return null;
            else return cachResponse;

        }
    }
}
