using System;
using System.Collections.Generic;

namespace GajGamesServiceRouter.Infrastructure.Dtos
{
    public class StudioDto
    {
        public StudioDto()
        {
            StudioGames = new List<CatalogueGameDto>();
        }

        public string StudioName { get; set; }
        public DateTime Established { get; set; }
        public IList<CatalogueGameDto> StudioGames { get; set; }
    }
}
