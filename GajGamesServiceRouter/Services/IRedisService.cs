using System;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Enums;

namespace GajGamesServiceRouter.Services
{
    public interface IRedisService
    {
        Task<bool> SetKey(string key, string value);
        Task<string> GetKeyValue(string key);
        Task<string> GenerateFilterKey(string userNick, RedisNamespace redisNamespace);

    }
}
