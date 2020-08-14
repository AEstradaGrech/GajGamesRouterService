using System;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;

namespace GajGamesServiceRouter.Services
{
    public interface ICartMgmtService
    {
        Task<CartDto> GetTestRedisCart();
        Task<bool> SetTestRedisCart();
        Task<CartDto> GetUserRedisCart();
        Task<CartDto> InitCustomerCart();
        Task<bool> SetUserCart(CartDto userCart);
        Task<bool> AddProductToCart(ProductDto product);
        Task<bool> RemoveProductFromCart(string productId);
    }
}
