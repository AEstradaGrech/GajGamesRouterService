using System;
namespace GajGamesServiceRouter.Infrastructure
{
    public class RedisConfiguration
    {
        public RedisConfiguration()
        {
        }

        public string Host { get; set; }
        public string Port { get; set; }
    }
}
