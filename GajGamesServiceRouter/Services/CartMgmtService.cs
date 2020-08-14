using System;
using System.Linq;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;
using GajGamesServiceRouter.Infrastructure.Enums;
using GajGamesServiceRouter.Infrastructure.Helpers;

namespace GajGamesServiceRouter.Services
{
    public class CartMgmtService : ICartMgmtService
    {
        private readonly IRedisService _redisService;
        private readonly IGajStoreMgmtService _gajStoreMgmtService;

        public CartMgmtService(IRedisService redisService, IGajStoreMgmtService gajStoreMgmtService)
        {
            _redisService = redisService;
            _gajStoreMgmtService = gajStoreMgmtService;
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

        public async Task<bool> AddProductToCart(ProductDto product)
        {            
            var cart = await GetUserRedisCart();

            if (cart != null)
            {
                cart.Products.Add(product);

                cart.TotalPrice += product.Price;

                var key = await _redisService.GenerateUserRedisKey(RedisNamespace.UserCart);

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

                    return await _redisService.SetKey(key, cart);
                }
            }

            return false;
        }
    }
}
