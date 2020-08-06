using System;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.ApiConfigurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace GajGamesServiceRouter.Services
{
    public class RestServiceBase<TConfig> where TConfig : ApiConfiguration, new()
    {
        private readonly TConfig _apiConfig;
        public  TConfig ApiConfig { get => _apiConfig; }

        private HttpContext _httpContext;
        public HttpContext HttpContext { get => _httpContext; }
   
        public RestServiceBase(IOptions<TConfig> apiConfig, IHttpContextAccessor contextAccessor)
        {
            _apiConfig = apiConfig.Value;
            _httpContext = contextAccessor.HttpContext;
        }

        protected async Task<string> GetAuthToken(HttpRequest request)
        {
            StringValues authHeader;
            request.Headers.TryGetValue("Authorization", out authHeader);
            return authHeader;
        }
    }
}
