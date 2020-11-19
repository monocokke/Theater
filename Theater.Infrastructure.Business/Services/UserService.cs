using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Services.Interfaces;

namespace Theater.Infrastructure.Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager) {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Register(UserDTO userDTO)
        {
            var user = await _userManager.FindByEmailAsync(userDTO.Email);

            if (user == null)
            {
                user = new User
                {
                    Email = userDTO.Email,
                    UserName = userDTO.Username,
                    BirthDate = userDTO.BirthDate,
                    Sex = userDTO.Sex
                };

                var registerResult = await _userManager.CreateAsync(user, userDTO.Password);

                var userRole = await _roleManager.FindByNameAsync("employee");

                if (userRole == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole("employee"));
                    userRole = await _roleManager.FindByNameAsync("employee");
                }

                await _userManager.AddToRoleAsync(user, userRole.Name);

                if (registerResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> Login(UserDTO userDTO)
        {
            var user = await _userManager.FindByEmailAsync(userDTO.Email);

            if (user != null)
            {
                var loginResult = await _signInManager.CheckPasswordSignInAsync(user, userDTO.Password, false);

                if (loginResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return true;
                }
            }
            return false;
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
