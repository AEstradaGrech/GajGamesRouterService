using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.ApiConfigurations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using RestSharp;

namespace GajGamesServiceRouter.Services
{
    public class RestServiceBase<TConfig> where TConfig : ApiConfiguration, new()
    {
        private readonly TConfig _apiConfig;
        public  TConfig ApiConfig { get => _apiConfig; }

        private HttpContext _httpContext;
        public HttpContext HttpContext { get => _httpContext; }

        private Dictionary<string, string> _queryParams;
        public Dictionary<string, string> QueryParams { get => _queryParams; set => _queryParams = value; }


        public RestServiceBase(IOptions<TConfig> apiConfig, IHttpContextAccessor contextAccessor)
        {
            _apiConfig = apiConfig.Value;
            _httpContext = contextAccessor.HttpContext;
            _queryParams = new Dictionary<string, string>();
        }

        protected async Task<string> GetAuthToken(HttpRequest request)
        {
            StringValues authHeader;
            request.Headers.TryGetValue("Authorization", out authHeader);
            return authHeader;
        }

        protected async Task<T> GetTAsync<T>(string url)
        {            
            var token = await GetAuthToken(_httpContext.Request);

            var restClient = new RestClient(_apiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest(url, Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddHeader("Authorization", token);            

            foreach (var key in _queryParams.Keys)
            {
                restReq.AddParameter(key, _queryParams[key]);
            }

            Console.WriteLine($"GET <T> ASYNC :: PARAMS :: {_queryParams} :: URI --> {restClient.BuildUri(restReq)}");            

            return await restClient.GetAsync<T>(restReq);
        }

        protected async Task<T>PostTAsync<T>(string url, object body)
        {
            var token = await GetAuthToken(_httpContext.Request);

            var restClient = new RestClient(_apiConfig.BaseUrl);

            var restReq = new RestRequest(url, Method.POST);

            restReq.AddJsonBody(body)
                   .AddHeader("Authorization", token);

            Console.WriteLine($"POST <T> ASYNC :: BODY :: {body} :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.PostAsync<T>(restReq);
        }
    
    }
}
