using System;
using System.Threading.Tasks;
using StackExchange.Redis;
using Newtonsoft.Json;
using GajGamesServiceRouter.Infrastructure.Dtos;
using GajGamesServiceRouter.Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using System.Linq;
using GajGamesServiceRouter.Infrastructure;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace GajGamesServiceRouter.Services
{
    public class RedisService : IRedisService
    {
        private RedisConfiguration _redisConfiguration;
        private IConnectionMultiplexer _redisMultiplexer;
        private IHttpContextAccessor _httpContextAccessor;        

        public RedisService(IOptions<RedisConfiguration> redisOptions, IHttpContextAccessor httpContextAccessor)
        {
            _redisConfiguration = redisOptions.Value;
            _httpContextAccessor = httpContextAccessor;                        
            _redisMultiplexer = ConnectionMultiplexer.Connect($"{_redisConfiguration.Host}:{_redisConfiguration.Port}");
        }

        private IDatabase GetDataBase() => _redisMultiplexer.GetDatabase();

        public async Task<string> GenerateUserRedisKey(RedisNamespace redisNamespace)
        {
            var subClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"));

            return $"{subClaim.Value}-{redisNamespace}";
        }

        public async Task<string> GetKeyStringValue(string key)
        {                      
            return await GetDataBase().StringGetAsync(key);
        }

        public async Task<bool> SetStringKey(string key, string value)
        {                                    
            var db = _redisMultiplexer.GetDatabase();

            return await db.StringSetAsync(key, value);            
        }

        public async Task<bool> SetKey<T>(string key, T value)
        {
            var jsonValue = JsonConvert.SerializeObject(value);

            return await GetDataBase().StringSetAsync(key, jsonValue);
        }

        public async Task<T> GetKeyValue<T>(string key)
        {
            var data = await GetDataBase().StringGetAsync(key);

            if (!TryDeserialize(data, out T result))
                await DeleteKey(key);    
            
            return result;
        }

        public Task<List<T>> GetKeyCollection<T>(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteKey(string key)
        {
            return await GetDataBase().KeyDeleteAsync(key);
        }

        private bool TryDeserialize<T>(RedisValue data, out T result)
        {
            result = default(T);

            if(!data.HasValue)                            
                return false;

            try
            {
                result = JsonConvert.DeserializeObject<T>(data);
                return true;
            }
            catch(JsonReaderException exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }            
        }
    }
}
