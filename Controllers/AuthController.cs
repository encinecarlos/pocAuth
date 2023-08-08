using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Poc.Authentication.Models;
using Poc.Authentication.Services;

namespace Poc.Authentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private TokenService TokenService { get; init; }

        public AuthController(TokenService tokenService)
        {
            TokenService = tokenService;
        }

        [HttpPost]
        public ActionResult Authenticate([FromBody] User request) 
        {
            var token = TokenService.GenerateToken(request);

            return Ok(new { Token = token });
        }
    }
}