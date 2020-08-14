using System;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;

namespace GajGamesServiceRouter.Services
{
    public interface IGajUsersRestService
    {
        Task<UserDto> GetUserByNickname(string userNick);        
    }
}
