using System;
using System.Collections.Generic;
using GajGamesServiceRouter.Infrastructure.Dtos;

namespace GajGamesServiceRouter.Infrastructure.Helpers
{
    public static class RedisFakeData
    {

        public static CartDto GetTestRedisCart()
        {
            var zeroGuid = new Guid();

            return new CartDto
            {
                Id = null,
                CustomerId = zeroGuid,
                CustomerAccountId = null,
                CreationDate = DateTime.Now,
                Products = new List<ProductDto>
                {
                    new ProductDto
                    {
                        CartId = null,
                        StoreProductId = zeroGuid,
                        ProductName = "ProductNumberOne",
                        Price = 10.0m
                    },
                    new ProductDto
                    {
                        CartId = null,
                        StoreProductId = zeroGuid,
                        ProductName = "ProductNumberTwo",
                        Price = 20.0m
                    }

                },
                TotalPrice = 30.0m
            };

        }
    }
}
