using System;
using System.Collections.Generic;
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
    public class ImagesController : ControllerBase
    {
        private readonly IGajImgsRestService _gajImgsRestService;

        public ImagesController(IGajImgsRestService gajImgsRestService)
        {
            _gajImgsRestService = gajImgsRestService;
        }


        [HttpPost]
        [Route("post-img")]
        [Authorize(Policy = "Customers")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostImage([FromBody]ImageDto dto)
        {                             
            var response = await _gajImgsRestService.PostImage(dto);

            if (response != null)
                return Ok(response);         
            
            return BadRequest();
        }

        [HttpGet]
        [Route("get-user-img")]
        [Authorize(Policy = "Customers")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserImage([FromQuery]Guid userGuid)
        {                                    
            var response = await _gajImgsRestService.GetUserImage(userGuid);

            if (response != null)
                return Ok(response);
            
            return BadRequest();
        }

        [HttpGet]
        [Route("get-test-dto")]
        [Authorize(Policy ="Anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTestDto()
        {

            var response = await _gajImgsRestService.GetTestDto();

            if (response != null)
                return Ok(response);         

            return BadRequest();
        }
   
    }
}
