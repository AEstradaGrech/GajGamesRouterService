using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.ApiConfigurations;
using GajGamesServiceRouter.Infrastructure.Dtos;
using Microsoft.Extensions.Options;
using RestSharp;

namespace GajGamesServiceRouter.Services
{
    public class GajImgsRestService : RestServiceBase<GajImgsApiConfiguration>, IGajImgsRestService
    {
        public GajImgsRestService(IOptions<GajImgsApiConfiguration> apiConfig) : base(apiConfig)
        {
        }

        public async Task<ImageDto> GetGameImageByGameTitle(string gameTitle, string authToken)
        {
            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Images")}/get-game-img", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddParameter("gameTitle", $"{gameTitle}")
                   .AddHeader("Authorization", authToken);

            Console.WriteLine($"GET GAME IMG :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.GetAsync<ImageDto>(restReq);
        }
    

        public async Task<IEnumerable<ImageDto>> GetGamesImageByGameTitle(IEnumerable<string> gameTitles, string authToken)
        {
            var restClient = new RestClient(ApiConfig.BaseUrl);

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Images")}/get-catalogue-imgs", Method.POST);

            restReq.AddJsonBody(gameTitles);
                  // .AddHeader("Authorization", authToken);
            
            Console.WriteLine($"GET GAME IMGS URI :: {restClient.BuildUri(restReq)}");
            
            return await restClient.PostAsync<IEnumerable<ImageDto>>(restReq);
        }

        public async Task<ImageDto> GetTestDto()
        {
            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Images")}/get-test-dto", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            Console.WriteLine($"GETTING TEST DTO :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.GetAsync<ImageDto>(restReq);
        }

        public async Task<ImageDto> GetUserImage(Guid userGuid, string userToken)
        {
            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Images")}/get-user-img", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddParameter("userGuid", $"{userGuid}")
                   .AddHeader("Authorization", userToken);

            Console.WriteLine($"GET USER IMG :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.GetAsync<ImageDto>(restReq);
        }

        public async Task<ImageDto> PostImage(ImageDto dto, string userToken)
        {
            var restClient = new RestClient(ApiConfig.BaseUrl);

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Images")}/post-img", Method.POST);

            restReq.AddJsonBody(dto)
                   .AddHeader("Authorization", userToken);            
                        
            return await restClient.PostAsync<ImageDto>(restReq);
            
        }
    }
}
