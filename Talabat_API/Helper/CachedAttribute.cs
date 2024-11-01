using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat_Core.Repositories_InterFaces;

namespace Talabat_API.Helper
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int expireTimeInSecond;

        public CachedAttribute(int ExpireTimeInSecond)
        {
            expireTimeInSecond = ExpireTimeInSecond;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var cache= context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
           var cachkey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
           var responsecahce=await cache.GetCachedResponse(cachkey);
            if (!string.IsNullOrEmpty(responsecahce))
            {
                var contentResult = new ContentResult() 
                { 
                    Content=responsecahce,
                    ContentType="application/json",
                    StatusCode=200,
                };
                context.Result=contentResult;
                return;

            }

           var ExecutedEndpint=await next.Invoke();
           

           if (ExecutedEndpint.Result is OkObjectResult okObjectResult)
            {
               await cache.CacheResponseAsync(cachkey, okObjectResult.Value,TimeSpan.FromSeconds(expireTimeInSecond) );
            }

        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();
            KeyBuilder.Append(request.Path);
            foreach(var (Key,item) in request.Query.OrderBy(s=>s.Key))
            {
                KeyBuilder.Append($"|-{Key}-{item}");
            }
            return KeyBuilder.ToString();
        }
    }
}
