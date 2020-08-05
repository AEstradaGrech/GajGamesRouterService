using System;
using System.Threading.Tasks;

namespace GajGamesServiceRouter.Services
{
    public interface IRedisService
    {
        Task<bool> SetKey(string key, string value);
        Task<string> GetKeyValue(string key);
    }
}
