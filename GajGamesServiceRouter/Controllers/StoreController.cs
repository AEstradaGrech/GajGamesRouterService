using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;
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
        private readonly IRedisService _redisService;

        public StoreController(IGajStoreMgmtService storeMgmtService, IRedisService redisService)
        {
            _storeMgmtService = storeMgmtService;
            _redisService = redisService;
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
        [Route("test-get-redis-key")]
        //[Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRedisKey([FromQuery]string key)
        {
            var response = await _redisService.GetKeyStringValue(key);

            if (!string.IsNullOrEmpty(response))
                return Ok(response);
            
            return BadRequest();
        }

        [HttpGet]
        [Route("test-set-redis-key")]
        [Authorize(Policy = "Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetRedisKey([FromQuery]string key, string value)
        {            
                  
            var claims = HttpContext.User.Claims.Where(c => c.Type.Contains("nameidentifier"));

            if (claims.Count() > 0)
                key += $"-{claims.FirstOrDefault().Value}";
         
            var response = await _redisService.SetStringKey(key, value);
            
            return Ok(response);            
        }        

    }
}
