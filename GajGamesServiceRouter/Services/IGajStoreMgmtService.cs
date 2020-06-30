using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;

namespace GajGamesServiceRouter.Services
{
    public interface IGajStoreMgmtService
    {
        Task<GameDetailDto> GetGameByGameId(Guid gameId, string authToken);
        Task<IEnumerable<CatalogueGameDto>> GetStudioByName(string studioName);
    }
}
