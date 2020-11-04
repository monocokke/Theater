using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theater.Domain.Core.Models;
using Theater.Domain.Core.DTO;
using Theater.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Theater.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserService service, ILogger<AccountController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [Route("/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation("Attempt to register new employee");
            if (ModelState.IsValid && await _service.Register(userDTO))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation("Attempt to login");
            if (ModelState.IsValid && await _service.Login(userDTO))
            {
                return Ok();
            }
            return BadRequest(HttpContext.Response);
        }

        [Route("/logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _service.Logout();
            return Ok();
        }
    }
}
