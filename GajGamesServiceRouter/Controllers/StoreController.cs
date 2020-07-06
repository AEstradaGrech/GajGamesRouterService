using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GajGamesServiceRouter.Infrastructure.Dtos;
using GajGamesServiceRouter.Services;
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

        public StoreController(IGajStoreMgmtService storeMgmtService)
        {
            _storeMgmtService = storeMgmtService;
        }

        [HttpGet]
        [Route("get-game-detail")]
        //[Authorize(Policy = "Customers")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetGameImage([FromQuery]Guid gameId)
        {
            var authToken = await GetAuthToken(Request);

            if (!string.IsNullOrEmpty(authToken))
            {
                var response = await _storeMgmtService.GetGameByGameId(gameId , authToken);

                if (response != null)
                    return Ok(response);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("get-by-studio-name")]
        //[Authorize(Policy = "Customers")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetGamesByStudioName([FromQuery]string studioName)
        {
            var authToken = await GetAuthToken(Request);

            if (!string.IsNullOrEmpty(authToken))
            {
                var response = await _storeMgmtService.GetByStudioName(studioName, authToken);

                if (response != null)
                    return Ok(response);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("get-studio-by-name")]
        //[Authorize(Policy = "Customers")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetStudioByName([FromQuery]string studioName)
        {
            var authToken = await GetAuthToken(Request);

            if (!string.IsNullOrEmpty(authToken))
            {
                var response = await _storeMgmtService.GetStudioByName(studioName, authToken);

                if (response != null)
                    return Ok(response);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("get-by-catalogue-filter")]
        //[Authorize(Policy = "Anonymous")]
        [ProducesResponseType(typeof(CatalogueResponseDto),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByFilter([FromBody]CatalogueFilter filter)
        {
            // var authToken = await GetAuthToken(Request);
            var authToken = "test";

            if (!string.IsNullOrEmpty(authToken))
            {
                var response = await _storeMgmtService.GetByFilter(filter, authToken);

                if (response != null)
                    return Ok(response);
            }

            return BadRequest();
        }


        private async Task<string> GetAuthToken(HttpRequest request)
        {
            StringValues authHeader;
            request.Headers.TryGetValue("Authorization", out authHeader);
            return authHeader;
        }

    }
}
