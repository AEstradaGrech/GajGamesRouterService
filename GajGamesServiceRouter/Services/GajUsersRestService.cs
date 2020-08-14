using System;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.ApiConfigurations;
using GajGamesServiceRouter.Infrastructure.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RestSharp;

namespace GajGamesServiceRouter.Services
{
    public class GajUsersRestService : RestServiceBase<GajUsersApiConfiguration>, IGajUsersRestService
    {
        public GajUsersRestService(IOptions<GajUsersApiConfiguration> options,
            IHttpContextAccessor contextAccessor) : base (options, contextAccessor)
        {
        }

        public async Task<UserDto> GetUserByNickname(string userNick)
        {
            var token = await GetAuthToken(HttpContext.Request);

            var restClient = new RestClient(ApiConfig.BaseUrl);

            restClient.UseJson();

            var restReq = new RestRequest($"{ApiConfig.EndpointByKey("Users")}/get-by-nickname", Method.GET);

            restReq.RequestFormat = DataFormat.Json;

            restReq.AddParameter("userNick", $"{userNick}")
                   .AddHeader("Authorization", token);

            Console.WriteLine($"GET USER BY NICKNAME:: URI --> {restClient.BuildUri(restReq)}");

            return await restClient.GetAsync<UserDto>(restReq);
        }
    }
}
