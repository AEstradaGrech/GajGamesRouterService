using System;
using System.Collections.Generic;
using GajGamesServiceRouter.Infrastructure.Enums;

namespace GajGamesServiceRouter.Infrastructure.Dtos
{
    public class CatalogueGameDto
    {
        public CatalogueGameDto()
        {
            GamePromotions = new List<GamePromotionDto>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }        
        public decimal? Price { get; set; }
        public PEGI PEGI { get; set; }
        public List<GamePromotionDto> GamePromotions { get; set; }
        public string GameImgB64 { get; set; }

    }
}
