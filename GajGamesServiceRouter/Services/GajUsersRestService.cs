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
    public class GajUsersRestService : RestServiceBase<GajUsersApiConfiguration>, IGajUsersRestService
    {
        public GajUsersRestService(IOptions<GajUsersApiConfiguration> options,
            IHttpContextAccessor contextAccessor) : base (options, contextAccessor)
        {
        }

        public async Task<UserDto> GetUserByNickname(string userNick)
        {                      
            QueryParams[nameof(userNick)] = userNick;            

            return await GetTAsync<UserDto>($"{ApiConfig.EndpointByKey("Users")}/get-by-nickname");                 
        }

        public async Task<Guid> GetUserIdByNickname(string nickName)
        {            
            QueryParams[nameof(nickName)] = nickName;            

            return await GetTAsync<Guid>($"{ApiConfig.EndpointByKey("Users")}/get-userId-by-nickname");
        }
    }
}
