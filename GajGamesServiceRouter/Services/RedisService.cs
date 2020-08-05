using System;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;
using GajGamesServiceRouter.Infrastructure.Dtos;

namespace GajGamesServiceRouter.Services
{
    public class RedisService : IRedisService
    {
        public RedisService()
        {
        }

        public async Task<string> GetKeyValue(string key)
        {                        
            var redis = ConnectionMultiplexer.Connect("gaj-redis:6379");
            var db = redis.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task<bool> SetKey(string key, string value)
        {
            var redis = ConnectionMultiplexer.Connect("gaj-redis:6379");
            var db = redis.GetDatabase();
            return await db.StringSetAsync(key, value);            
        }
    }
}
