using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;

namespace GajGamesServiceRouter.Services
{
    public interface IGajStoreRestService
    {
        Task<GameDetailDto> GetGameByGameId(Guid gameId, string authToken);
        Task<IEnumerable<CatalogueGameDto>> GetByStudioName(string studioName, string authToken);
        Task<StudioDto> GetStudioByName(string studioName, string authToken);
        Task<CatalogueResponseDto> GetByFilter(CatalogueFilter filter, string authToken);
    }
}
