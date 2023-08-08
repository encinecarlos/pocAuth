using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Poc.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetDate()
        {
            var datetime = new DateTime(2023, 07, 28);

            return Ok(new { FutureDate = datetime.AddDays(20).ToString("dd/MM/yyyy") });
        }

        [HttpGet("profile")]
        [Authorize]
        public ActionResult GetProfile()
        {
            return Ok($"profile loaded: {User.Identity?.Name ?? string.Empty}");
        }
    }
}