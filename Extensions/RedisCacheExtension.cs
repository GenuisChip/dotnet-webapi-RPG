using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace dotnet_rpg.Extensions
{
    public static class RedisCacheExtension
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,
        string recordId,
        T data,
        TimeSpan? expireTime = null,
        TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = expireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = unusedExpireTime;
            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId,jsonData,options);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache,
        string recordId){
            var jsonData = await cache.GetStringAsync(recordId);
            if(jsonData is null){
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}