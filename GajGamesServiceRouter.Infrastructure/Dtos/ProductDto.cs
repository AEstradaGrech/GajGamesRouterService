using System;
namespace GajGamesServiceRouter.Infrastructure.Dtos
{
    public class ProductDto
    { 
        public Guid? CartId { get; set; }
        public Guid StoreProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
