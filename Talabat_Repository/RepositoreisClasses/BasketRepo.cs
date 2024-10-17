using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat_Core.Models;
using Talabat_Core.Repositories_InterFaces;

namespace Talabat_Repository.RepositoreisClasses
{
    public class BasketRepo:IBasketRepo
    {
        private readonly IDatabase database;

        public BasketRepo(IConnectionMultiplexer redis)
        {
            database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket =await database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);

        }

        public  async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var baskeUpdateOrCreate=await database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
            if (baskeUpdateOrCreate is false) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
