using System;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.ApiConfigurations;
using GajGamesServiceRouter.Infrastructure.Dtos;
using Microsoft.Extensions.Options;

namespace GajGamesServiceRouter.Services
{
    public class GajUsersRestService : RestServiceBase<GajUsersApiConfiguration>, IGajUsersRestService
    {
        public GajUsersRestService(IOptions<GajUsersApiConfiguration> options) : base (options)
        {
        }

        public async Task<UserDto> GetUserByNickname(string userNick)
        {
            throw new NotImplementedException();
        }
    }
}
