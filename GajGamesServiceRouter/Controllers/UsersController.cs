using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GajGamesServiceRouter.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GajGamesServiceRouter.Controllers
{
    [Route("api/v1/rtr/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IGajUsersRestService _gajUsersRestService;

        public UsersController(IGajUsersRestService gajUsersRestService)
        {
            _gajUsersRestService = gajUsersRestService;
        }

        [HttpGet]
        [Route("get-user-by-nickame")]
        [Authorize(Policy="Customers")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult>GetUserByNickname([FromQuery]string userNick)
        {
            var response = await _gajUsersRestService.GetUserByNickname(userNick);

            if (response != null)
                return Ok(response);

            return BadRequest();
        }
    }
}
