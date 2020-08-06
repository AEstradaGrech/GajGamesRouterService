using System;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;
using GajGamesServiceRouter.Infrastructure.Dtos;
using GajGamesServiceRouter.Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace GajGamesServiceRouter.Services
{
    public class RedisService : IRedisService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public RedisService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<string> GenerateFilterKey(string userNick, RedisNamespace redisNamespace)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetKeyValue(string key)
        {
            var subClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"));
            var redis = ConnectionMultiplexer.Connect("gaj-redis:6379");
            var db = redis.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task<bool> SetKey(string key, string value)
        {
            var subClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"));
            Console.WriteLine($"HTTP CONTEXT ACCESSOR SUB CLAIM :: {subClaim}");
            var redis = ConnectionMultiplexer.Connect("gaj-redis:6379");
            var db = redis.GetDatabase();
            return await db.StringSetAsync(key, value);            
        }
    }
}
