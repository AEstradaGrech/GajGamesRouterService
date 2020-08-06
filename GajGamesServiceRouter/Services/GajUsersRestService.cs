using System;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.ApiConfigurations;
using GajGamesServiceRouter.Infrastructure.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

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
            throw new NotImplementedException();
        }
    }
}
