using System;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;

namespace GajGamesServiceRouter.Services
{
    public interface IGajImgsRestService
    {
        Task<ImageDto> PostImage(ImageDto dto, string userToken);
        Task<ImageDto> GetUserImage(Guid userGuid, string userToken);
        Task<ImageDto> GetTestDto();
    }
}
