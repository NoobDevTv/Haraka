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
    public class JobController : ControllerBase
    {
        private readonly JobService jobService;
        private readonly GameService gameService;

        public JobController(JobService jobService, GameService gameService)
        {
            this.jobService = jobService;
            this.gameService = gameService; //HACK: to instantiate as singelton
        }

        [HttpPost("[action]")]
        public IActionResult Create(JobInfo jobInfo)
        {
            if (jobService.TryCreate(jobInfo))
                return Ok();
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
