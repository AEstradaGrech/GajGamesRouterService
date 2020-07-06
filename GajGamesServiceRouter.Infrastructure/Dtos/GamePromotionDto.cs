using System;
using GajGamesServiceRouter.Infrastructure.Enums;

namespace GajGamesServiceRouter.Infrastructure.Dtos
{
    public class GamePromotionDto
    {
        public string Description { get; set; }
        public int? Discount { get; set; }
        public AccountTypes? AccountType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
