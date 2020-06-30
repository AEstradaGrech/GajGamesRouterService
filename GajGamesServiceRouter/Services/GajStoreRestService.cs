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
        public GajStoreRestService(IOptions<GajStoreApiConfiguration>options) : base(options)
        {
        }

        public async Task<GameDetailDto> GetGameByGameId(Guid gameId, string authToken)
        {
            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Games")}/get-by-gameId", Method.GET);

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
    }
}
