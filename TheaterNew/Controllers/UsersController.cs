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
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return new ObjectResult(await _userManager.Users.ToListAsync());
        }

        [Route("/create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTO userDTO)
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
                    await _userManager.AddToRoleAsync(user, userDTO.Role);
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
            return BadRequest(ModelState);
        }        
    }
}
