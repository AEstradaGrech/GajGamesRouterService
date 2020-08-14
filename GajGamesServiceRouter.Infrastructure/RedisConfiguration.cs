using System;
namespace GajGamesServiceRouter.Infrastructure
{
    public class RedisConfiguration
    {
        public RedisConfiguration()
        {
            DefaultKeyExpiration = new TimeSpan(0, 0, 1, 0);
        }

        public string Host { get; set; }
        public string Port { get; set; }
        public TimeSpan DefaultKeyExpiration { get; set; }
    }
}
