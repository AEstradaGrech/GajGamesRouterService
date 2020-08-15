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
    public class GajStoreRestService : RestServiceBase<GajStoreApiConfiguration>, IGajStoreRestService
    {
        public GajStoreRestService(IOptions<GajStoreApiConfiguration>apiConfig,
            IHttpContextAccessor contextAccessor) : base(apiConfig, contextAccessor)
        {
        }

        public async Task<GameDetailDto> GetGameByGameId(Guid gameId)
        {
            QueryParams[nameof(gameId)] = gameId.ToString();

            return await GetTAsync<GameDetailDto>($"{ApiConfig.EndpointByKey("Games")}/get-game-detail");
        }

        public async Task<IEnumerable<CatalogueGameDto>> GetByStudioName(string studioName)
        {
            QueryParams[nameof(studioName)] = studioName;

            return await GetTAsync<IEnumerable<CatalogueGameDto>>($"{ApiConfig.EndpointByKey("Games")}/get-by-studio-name");
        }

        public async Task<StudioDto> GetStudioByName(string studioName)
        {
            QueryParams[nameof(studioName)] = studioName;

            return await GetTAsync<StudioDto>($"{ApiConfig.EndpointByKey("Studios")}/get-studio-by-name");
        }

        public async Task<CatalogueResponseDto> GetByFilter(CatalogueFilter filter)
        {
            return await PostTAsync<CatalogueResponseDto>($"{ApiConfig.EndpointByKey("Games")}/get-by-catalogue-filter", filter);
        }

        public async Task<IEnumerable<string>> GetStudioNames()
        {
            return await GetTAsync<IEnumerable<string>>($"{ApiConfig.EndpointByKey("Studios")}/get-studio-names");
        }

        public async Task<IEnumerable<string>> GetGameGenres()
        {
            return await GetTAsync<IEnumerable<string>>($"{ApiConfig.EndpointByKey("Games")}/get-game-genres");
        }
    }
}
