using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;

namespace GajGamesServiceRouter.Services
{
    public interface IGajImgsRestService
    {
        Task<ImageDto> PostImage(ImageDto dto);
        Task<ImageDto> GetUserImage(Guid userGuid);
        Task<ImageDto> GetTestDto();
        Task<ImageDto> GetGameImageByGameTitle(string gameTitle);
        Task<IEnumerable<ImageDto>> GetGameImagesByGameTitle(IEnumerable<string> gameTitles);
    }
}
