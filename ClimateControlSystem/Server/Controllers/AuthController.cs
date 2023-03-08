﻿using ClimateControl.Server.Infrastructure.Services;
using ClimateControl.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ClimateControl.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticateManager _authManager;

        public AuthController(IAuthenticateManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var token = await _authManager.GetTokenForUser(request);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(string.Empty);
            }

            return Ok(token);
        }
    }
}
