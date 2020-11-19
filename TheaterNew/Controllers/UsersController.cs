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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Theater.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all users (for admins)");
            return Ok(await _userManager.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation("Create user (for admins)");
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
                    var userRole = await _roleManager.FindByNameAsync(userDTO.Role);

                    if (userRole == null)
                    {
                        var roleResult = await _roleManager.CreateAsync(new IdentityRole(userDTO.Role));
                        if (roleResult.Succeeded)
                        {
                            userRole = await _roleManager.FindByNameAsync(userDTO.Role);
                        }
                        else return BadRequest(roleResult.Errors);
                    }

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

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            _logger.LogInformation($"Get user by email: {email} (for admins)");
            return Ok(await _userManager.FindByEmailAsync(email));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation($"Delete user by email: {id} (for admins)");
            if (ModelState.IsValid)
            {
                if (id != null)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    await _userManager.DeleteAsync(user);
                }
            }
            return BadRequest();
        }
    }
}
