using System;
using System.Collections.Generic;

namespace GajGamesServiceRouter.Infrastructure.Dtos
{
    public class CatalogueResponseDto
    {
        public CatalogueResponseDto()
        {
            Games = new List<CatalogueGameDto>();
        }

        public List<CatalogueGameDto> Games { get; set; }
        public int TotalCount { get; set; }
    }
}
