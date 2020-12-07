using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theater.Domain.Core.Entities;
using Theater.Domain.Core.DTO;
using Theater.Services.Interfaces;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Theater.Domain.Core.Models.User.Account;

namespace Theater.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(IUserService service, ILogger<AccountController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
        {
            _logger.LogInformation("Attempt to register new employee");
            if (ModelState.IsValid && await _service.Register(_mapper.Map<UserDTO>(model)))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserModel model)
        {
            _logger.LogInformation("Attempt to login");
            if (ModelState.IsValid && await _service.Login(_mapper.Map<UserDTO>(model)))
            {
                return Ok();
            }
            return BadRequest(HttpContext.Response);
        }

        [Route("/logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("User logout");
            await _service.Logout();
            return Ok();
        }
    }
}
