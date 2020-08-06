using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;

namespace GajGamesServiceRouter.Services
{
    public interface IGajStoreRestService
    {
        Task<GameDetailDto> GetGameByGameId(Guid gameId);
        Task<IEnumerable<CatalogueGameDto>> GetByStudioName(string studioName);
        Task<StudioDto> GetStudioByName(string studioName);
        Task<CatalogueResponseDto> GetByFilter(CatalogueFilter filter);
        Task<IEnumerable<string>> GetStudioNames();
        Task<IEnumerable<string>> GetGameGenres();
    }
}
