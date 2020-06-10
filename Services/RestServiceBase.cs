using System;
using GajGamesServiceRouter.Infrastructure.ApiConfigurations;
using Microsoft.Extensions.Options;

namespace GajGamesServiceRouter.Services
{
    public class RestServiceBase<TConfig> where TConfig : ApiConfiguration, new()
    {
        private readonly TConfig _apiConfig;
        public  TConfig ApiConfig { get => _apiConfig; }

        public RestServiceBase(IOptions<TConfig> apiConfig) 
        {
            _apiConfig = apiConfig.Value;
        }
    }
}
