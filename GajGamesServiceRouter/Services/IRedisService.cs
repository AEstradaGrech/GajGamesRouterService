using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Enums;

namespace GajGamesServiceRouter.Services
{
    public interface IRedisService
    {
        Task<bool> SetStringKey(string key, string value);
        Task<string> GetKeyStringValue(string key);
        Task<string> GenerateUserRedisKey(RedisNamespace redisNamespace);
        Task<bool> SetKey<T>(string key, T value);
        Task<T> GetKeyValue<T>(string key);
        Task<List<T>> GetKeyCollection<T>(string key);
        Task<bool> DeleteKey(string key);

    }
}
