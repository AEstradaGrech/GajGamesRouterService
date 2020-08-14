using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;
using GajGamesServiceRouter.Infrastructure.Enums;
using Newtonsoft.Json;

namespace GajGamesServiceRouter.Services
{
    public class GajStoreMgmtService : IGajStoreMgmtService
    {
        private readonly IGajStoreRestService _storeService;
        private readonly IGajImgsRestService _imgsService;
        private readonly ICartMgmtService _cartMgmtService;
        private readonly IRedisService _redisService;

        public GajStoreMgmtService(IGajStoreRestService storeServcice, IGajImgsRestService imgsService,
            IRedisService redisService, ICartMgmtService cartMgmtService)
        {
            _storeService = storeServcice;
            _imgsService = imgsService;
            _redisService = redisService;
            _cartMgmtService = cartMgmtService;
        }

        public async Task<GameDetailDto> GetGameByGameId(Guid gameId)
        {
            var game = await _storeService.GetGameByGameId(gameId);

            if(game != null)
            {
                var gameImg = await _imgsService.GetGameImageByGameTitle(game.Title);

                if (gameImg != null)
                    game.GameImgB64 = gameImg.ImgBase64;
            }
            
            return game;
        }

        public async Task<IEnumerable<CatalogueGameDto>> GetByStudioName(string studioName)
        {
            var games = await _storeService.GetByStudioName(studioName);

            if(games != null || games.Count() > 0)
            {
                var gameTitles = games.Select(g => g.Title);

                var imgs = await _imgsService.GetGameImagesByGameTitle(gameTitles);

                games = await AddImgsToGames(games, imgs);                
            }

            return games;
        }

        private async Task<IEnumerable<CatalogueGameDto>> AddImgsToGames(IEnumerable<CatalogueGameDto> games, IEnumerable<ImageDto> imgs)
        {
            games.ToList().ForEach(g =>
            {
                if(imgs.Any(img => img.ImgName == g.Title))
                {
                    g.GameImgB64 = imgs.SingleOrDefault(img => img.ImgName == g.Title).ImgBase64;
                }
            });

            return games;
        }

        public async Task<StudioDto> GetStudioByName(string studioName)
        {
            var studio = await _storeService.GetStudioByName(studioName);

            if(studio != null)
            {
                var imgs = await _imgsService.GetGameImagesByGameTitle(studio.StudioGames.Select(g => g.Title));

                if(imgs != null || imgs.Count() > 0)
                {
                    studio.StudioGames = await AddImgsToGames(studio.StudioGames, imgs) as List<CatalogueGameDto>;
                }
            }

            return studio;
        }

        public async Task<CatalogueResponseDto> GetByFilter(CatalogueFilter filter)
        {
            var catalogueResponse = await _storeService.GetByFilter(filter);           

            if(catalogueResponse != null && catalogueResponse.Games.Count() > 0)
            {
                var imgNames = catalogueResponse.Games.Select(g => g.Title);

                var images = await _imgsService.GetGameImagesByGameTitle(imgNames);                

                var dtosWithImg = await AddImgsToGames(catalogueResponse.Games, images);

                catalogueResponse.Games = dtosWithImg.ToList();

                await CacheCatalogueResponse(catalogueResponse);
            }

            return catalogueResponse;
        }

        public async Task<IEnumerable<string>> GetStudioNames()
        {
            return await _storeService.GetStudioNames();
        }

        public async Task<IEnumerable<string>> GetGameGenres()
        {
            return await _storeService.GetGameGenres();
        }

        private async Task<bool> CacheCatalogueResponse(CatalogueResponseDto response)
        {
            var key = await _redisService.GenerateUserRedisKey(RedisNamespace.UserCart);
            
            return await _redisService.SetKey(key, response);
        }      
    }
}
