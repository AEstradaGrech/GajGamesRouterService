using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.ApiConfigurations;
using GajGamesServiceRouter.Infrastructure.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RestSharp;

namespace GajGamesServiceRouter.Services
{
    public class GajStoreRestService : RestServiceBase<GajStoreApiConfiguration>, IGajStoreRestService
    {
        public GajStoreRestService(IOptions<GajStoreApiConfiguration>apiConfig,
            IHttpContextAccessor contextAccessor) : base(apiConfig, contextAccessor)
        {
        }

        public async Task<GameDetailDto> GetGameByGameId(Guid gameId)
        {
            var token = await GetAuthToken(HttpContext.Request);

            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Games")}/get-game-detail", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddParameter("gameId", $"{gameId}")
                   .AddHeader("Authorization", token);

            Console.WriteLine($"GET GAME BY ID :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.GetAsync<GameDetailDto>(restReq);
        }

        public async Task<IEnumerable<CatalogueGameDto>> GetByStudioName(string studioName)
        {
            var token = await GetAuthToken(HttpContext.Request);

            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Games")}/get-by-studio-name", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddParameter("studioName", $"{studioName}")
                   .AddHeader("Authorization", token);

            Console.WriteLine($"GET GAMES BY STUDIO NAME :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.GetAsync<IEnumerable<GameDetailDto>>(restReq);
        }

        public async Task<StudioDto> GetStudioByName(string studioName)
        {
            var token = await GetAuthToken(HttpContext.Request);

            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Studios")}/get-studio-by-name", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddParameter("studioName", $"{studioName}")
                   .AddHeader("Authorization", token);

            Console.WriteLine($"GET STUDIO BY STUDIO NAME :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.GetAsync<StudioDto>(restReq);
        }

        public async Task<CatalogueResponseDto> GetByFilter(CatalogueFilter filter)
        {
            var token = await GetAuthToken(HttpContext.Request);

            var restClient = new RestClient(ApiConfig.BaseUrl);            

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Games")}/get-by-catalogue-filter", Method.POST);

            restReq.AddJsonBody(filter)
                   .AddHeader("Authorization", token);

            Console.WriteLine($"GET BY FILTER :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.PostAsync<CatalogueResponseDto>(restReq);
        }

        public async Task<IEnumerable<string>> GetStudioNames()
        {
            var token = await GetAuthToken(HttpContext.Request);

            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Studios")}/get-studio-names", Method.GET);            

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddHeader("Authorization", token);

            return await restClient.GetAsync<IEnumerable<string>>(restReq);
        }

        public async Task<IEnumerable<string>> GetGameGenres()
        {
            var token = await GetAuthToken(HttpContext.Request);

            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Games")}/get-game-genres", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddHeader("Authorization", token);

            return await restClient.GetAsync<IEnumerable<string>>(restReq);
        }
    }
}
