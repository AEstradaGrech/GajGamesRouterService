using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.ApiConfigurations;
using GajGamesServiceRouter.Infrastructure.Dtos;
using Microsoft.Extensions.Options;
using RestSharp;

namespace GajGamesServiceRouter.Services
{
    public class GajStoreRestService : RestServiceBase<GajStoreApiConfiguration>, IGajStoreRestService
    {
        public GajStoreRestService(IOptions<GajStoreApiConfiguration>apiConfig) : base(apiConfig)
        {
        }

        public async Task<GameDetailDto> GetGameByGameId(Guid gameId, string authToken)
        {
            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Games")}/get-game-detail", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddParameter("gameId", $"{gameId}")
                   .AddHeader("Authorization", authToken);

            Console.WriteLine($"GET GAME BY ID :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.GetAsync<GameDetailDto>(restReq);
        }

        public async Task<IEnumerable<CatalogueGameDto>> GetByStudioName(string studioName, string authToken)
        {
            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Games")}/get-by-studio-name", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddParameter("studioName", $"{studioName}")
                   .AddHeader("Authorization", authToken);

            Console.WriteLine($"GET GAMES BY STUDIO NAME :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.GetAsync<IEnumerable<GameDetailDto>>(restReq);
        }

        public async Task<StudioDto> GetStudioByName(string studioName, string authToken)
        {
            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Studios")}/get-studio-by-name", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddParameter("studioName", $"{studioName}")
                   .AddHeader("Authorization", authToken);

            Console.WriteLine($"GET STUDIO BY STUDIO NAME :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.GetAsync<StudioDto>(restReq);
        }

        public async Task<CatalogueResponseDto> GetByFilter(CatalogueFilter filter, string authToken)
        {
            var restClient = new RestClient(ApiConfig.BaseUrl);            

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Games")}/get-by-catalogue-filter", Method.POST);

            restReq.AddJsonBody(filter);
                   //.AddHeader("Authorization", authToken);

            Console.WriteLine($"GET BY FILTER :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.PostAsync<CatalogueResponseDto>(restReq);
        }
    }
}
