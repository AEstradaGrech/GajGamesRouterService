using System;
using System.Collections.Generic;

namespace GajGamesServiceRouter.Infrastructure.Dtos
{
    public class CartDto
    {
        public CartDto()
        {
            Products = new List<ProductDto>();
            TotalPrice = 0.0m;
            CreationDate = DateTime.Now;
        }

        public Guid? Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? CustomerAccountId { get; set; }
        public List<ProductDto> Products { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
