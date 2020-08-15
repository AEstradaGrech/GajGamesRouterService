using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;
using GajGamesServiceRouter.Infrastructure.Enums;
using GajGamesServiceRouter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GajGamesServiceRouter.Controllers
{
    [Route("api/v1/rtr/[controller]")]
    public class StoreController : Controller
    {
        private readonly IGajStoreMgmtService _storeMgmtService;
        private readonly ICartMgmtService _cartMgmtService;

        public StoreController(IGajStoreMgmtService storeMgmtService, ICartMgmtService cartMgmtService)
        {
            _storeMgmtService = storeMgmtService;
            _cartMgmtService = cartMgmtService;
        }

        [HttpGet]
        [Route("get-game-detail")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetGameDetail([FromQuery]Guid gameId)
        {                              
            var response = await _storeMgmtService.GetGameByGameId(gameId);

            if (response != null)
                return Ok(response);         

            return BadRequest();
        }

        [HttpGet]
        [Route("get-by-studio-name")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetGamesByStudioName([FromQuery]string studioName)
        {                                   
            var response = await _storeMgmtService.GetByStudioName(studioName);

            if (response != null)
                return Ok(response);            

            return BadRequest();
        }

        [HttpGet]
        [Route("get-studio-by-name")]
        [Authorize(Policy = "Customers")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetStudioByName([FromQuery]string studioName)
        {                                   
            var response = await _storeMgmtService.GetStudioByName(studioName);

            if (response != null)
                return Ok(response);            

            return BadRequest();
        }

        [HttpPost]
        [Route("get-by-catalogue-filter")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType(typeof(CatalogueResponseDto),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByFilter([FromBody]CatalogueFilter filter)
        {                                                
            var response = await _storeMgmtService.GetByFilter(filter);

            if (response != null)
                return Ok(response);          

            return BadRequest();
        }

        [HttpGet]
        [Route("get-studio-names")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetStudiNames()
        {                                 
            var response = await _storeMgmtService.GetStudioNames();

            if (response != null)
                return Ok(response);
           
            return BadRequest();
        }

        [HttpGet]
        [Route("get-game-genres")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetGameGenres()
        {            
            var response = await _storeMgmtService.GetGameGenres();

            if (response != null)
                return Ok(response);


            return BadRequest();
        }

        [HttpGet]
        [Route("get-user-redis-cart")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRedisCart()
        {
            var response = await _cartMgmtService.GetUserRedisCart();

            if (response != null)
                return Ok(response);

            return BadRequest();
        }

        [HttpPost]
        [Route("add-product-to-cart")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddProductToCart([FromBody]ProductDto product)
        {
            var response = await _cartMgmtService.AddProductToCart(product);

            return Ok(response);
        }

        [HttpDelete]
        [Route("remove-product-from-cart")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveProductFromCart([FromQuery]string productId)
        {
            var response = await _cartMgmtService.RemoveProductFromCart(productId);

            return Ok(response);
        }

        [HttpDelete]
        [Route("remove-all-from-cart")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveAllFromCart()
        {
            var response = await _cartMgmtService.RemoveAllFromCart();

            if(response != null)
                return Ok(response);

            return BadRequest();
        }

        [HttpPost]
        [Route("set-user-redis-cart")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetUserCart([FromBody]CartDto userCart)
        {
            var response = await _cartMgmtService.SetUserCart(userCart);

            return Ok(response);
        }

        [HttpGet]
        [Route("init-customer-cart")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> InitCustomerCart()
        {
            var response = await _cartMgmtService.InitCustomerCart();

            if (response != null)
                return Ok(response);

            return BadRequest();
        }

        [HttpGet]
        [Route("test-get-redis-key")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRedisKey()
        {
            var response = await _cartMgmtService.GetTestRedisCart();

            if (response != null)
                return Ok(response);
            
            return BadRequest();
        }

        [HttpGet]
        [Route("test-set-redis-key")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetRedisStringKey()
        {            
            var response = await _cartMgmtService.SetTestRedisCart();
            
            return Ok(response);            
        }

    }
}
