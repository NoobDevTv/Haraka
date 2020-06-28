using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Haraka.WebApp.Shared.Information;
using Haraka.WebApp.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Haraka.WebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserSessionService usersessionService;

        public AuthenticationController(IUserSessionService sessionService)
        {
            usersessionService = sessionService;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult Test()
        {
            return Ok();
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginUser(LoginInfo loginInfo)
        {
            return Ok(usersessionService.CreateToken(loginInfo));
        }
    }
}
