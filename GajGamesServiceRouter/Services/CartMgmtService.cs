using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;
using GajGamesServiceRouter.Infrastructure.Enums;
using GajGamesServiceRouter.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;

namespace GajGamesServiceRouter.Services
{
    public class CartMgmtService : ICartMgmtService
    {
        private readonly IRedisService _redisService;
        private readonly IGajUsersRestService _gajUsersRestService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartMgmtService(IRedisService redisService, IGajUsersRestService gajUsersRestService,
            IHttpContextAccessor httpContextAccessor)
        {
            _redisService = redisService;
            _gajUsersRestService = gajUsersRestService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CartDto> GetTestRedisCart()
        {
            var key = $"TestCart-{new Guid()}";

            return await _redisService.GetKeyValue<CartDto>(key);
        }

        public async Task<bool> SetTestRedisCart()
        {
            var cart = RedisFakeData.GetTestRedisCart();

            var key = $"TestCart-{new Guid()}";

            return await _redisService.SetKey(key, cart);
        }

        public async Task<CartDto> GetUserRedisCart()
        {
            var key = await _redisService.GenerateUserRedisKey(RedisNamespace.UserCart);

            return await _redisService.GetKeyValue<CartDto>(key);            
        } 

        public async Task<bool> SetUserCart(CartDto userCart)
        {
            var key = await _redisService.GenerateUserRedisKey(RedisNamespace.UserCart);

            return await _redisService.SetKey(key, userCart);            
        }

        private async Task<bool> IsAnonymousUser()
        {
            var subClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"));

            if(subClaim != null)
            {
                string[] claimSplitted = subClaim.Value.Split('_');

                if (claimSplitted[0] == "anon")
                    return true;
            }

            return false;
        }

        public async Task<bool> AddProductToCart(ProductDto product)
        {            
            var cart = await GetUserRedisCart();

            Console.WriteLine($"ADDING PRODUCT TO CART :: {cart}");

            if (cart != null)
            {
                Console.WriteLine($"PRODUCT :: {product}");

                cart.Products.Add(product);

                cart.TotalPrice += product.Price;

                var key = await _redisService.GenerateUserRedisKey(RedisNamespace.UserCart);

                Console.WriteLine($"ADDING PRODUCT TO CART :: {cart} :: KEY :: {key}");

                return await _redisService.SetKey(key, cart);
            }
            // TODO else create cart and add?
            return false;
        }

        public async Task<bool> RemoveProductFromCart(string productId)
        {
            var cart = await GetUserRedisCart();

            if (cart != null)
            {
                var productToRemove = cart.Products.FirstOrDefault(p => p.StoreProductId == new Guid(productId));

                if (productToRemove != null)
                {
                    cart.Products.Remove(productToRemove);

                    var key = await _redisService.GenerateUserRedisKey(RedisNamespace.UserCart);

                    Console.WriteLine($"REMOVING PRODUCT FROM CART :: {cart} :: KEY :: {key}");

                    return await _redisService.SetKey(key, cart);
                }
            }

            return false;
        }

        public async Task<CartDto> InitCustomerCart()
        {
            var userCart = new CartDto
            {
                Id = null,
                CustomerId = new Guid(),
                CustomerAccountId = null,
                CreationDate = DateTime.Now,
                Products = new List<ProductDto>(),
                TotalPrice = 0m
            };

            if (!await IsAnonymousUser())
            {
                var subClaim = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type.Contains("nameidentifier"));

                if (!string.IsNullOrEmpty(subClaim.Value))
                {
                    var userId = await _gajUsersRestService.GetUserIdByNickname(subClaim.Value);

                    if (userId != null)
                        userCart.CustomerId = userId;

                    //TODO: GetAccountId();
                }
            }

            var key = await _redisService.GenerateUserRedisKey(RedisNamespace.UserCart);

            var init = await _redisService.SetKey(key, userCart);

            if (init)
                return await _redisService.GetKeyValue<CartDto>(key);

            return null;
        }
    }
}
