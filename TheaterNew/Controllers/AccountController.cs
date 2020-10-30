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

namespace Theater.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [Route("/register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = userDTO.Email,
                    UserName = userDTO.Username,
                    BirthDate = userDTO.BirthDate,
                    Sex = userDTO.Sex
                };
                var result = await _userManager.CreateAsync(user, userDTO.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Ok(result.Succeeded);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return BadRequest();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] [Bind("Email", "Password")] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, true, false);
                if (result.Succeeded)
                {
                    return Ok(result.Succeeded);
                }
            }
            return BadRequest(HttpContext.Response);
        }

        [Route("/logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(true);
        }
    }
}
