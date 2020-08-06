using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.ApiConfigurations;
using GajGamesServiceRouter.Infrastructure.Dtos;
using GajGamesServiceRouter.Infrastructure.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RestSharp;

namespace GajGamesServiceRouter.Services
{
    public class GajImgsRestService : RestServiceBase<GajImgsApiConfiguration>, IGajImgsRestService
    {
        public GajImgsRestService(IOptions<GajImgsApiConfiguration> apiConfig,
            IHttpContextAccessor contextAccessor) : base(apiConfig, contextAccessor)
        {
        }

        public async Task<ImageDto> GetGameImageByGameTitle(string gameTitle)
        {
            var token = await GetAuthToken(HttpContext.Request);

            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Images")}/get-by-name-and-category", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddParameter("imgName", $"{gameTitle}")
                   .AddParameter("category", $"{ImgCategory.GamesCatalogue}")
                   .AddHeader("Authorization", token);

            Console.WriteLine($"GET GAME IMG :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.GetAsync<ImageDto>(restReq);
        }
    

        public async Task<IEnumerable<ImageDto>> GetGameImagesByGameTitle(IEnumerable<string> gameTitles)
        {
            var token = await GetAuthToken(HttpContext.Request);

            var restClient = new RestClient(ApiConfig.BaseUrl);

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Images")}/get-catalogue-imgs", Method.POST);

            restReq.AddJsonBody(gameTitles)
                   .AddHeader("Authorization", token);
            
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

        public async Task<ImageDto> GetUserImage(Guid userGuid)
        {
            var token = await GetAuthToken(HttpContext.Request);

            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Images")}/get-user-img", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddParameter("userGuid", $"{userGuid}")
                   .AddHeader("Authorization", token);

            Console.WriteLine($"GET USER IMG :: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.GetAsync<ImageDto>(restReq);
        }

        public async Task<ImageDto> PostImage(ImageDto dto)
        {
            var token = await GetAuthToken(HttpContext.Request);

            var restClient = new RestClient(ApiConfig.BaseUrl);

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Images")}/post-img", Method.POST);

            restReq.AddJsonBody(dto)
                   .AddHeader("Authorization", token);            
                        
            return await restClient.PostAsync<ImageDto>(restReq);
            
        }
    }
}
