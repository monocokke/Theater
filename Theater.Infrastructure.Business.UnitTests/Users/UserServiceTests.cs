using Microsoft.AspNetCore.Identity;
using Moq;
using Theater.Services.Interfaces;
using Theater.Domain.Core.Entities;
using NUnit.Framework;
using Theater.Infrastructure.Business.Services;
using AutoMapper;
using Theater.Domain.Core.DTO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Theater.Infrastructure.Business.UnitTests.Users
{
    public class UserServiceTests
    {
        #region Members

        private IUserService _service;
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IdentityResult> _mockIdentityResult;

        private List<UserDTO> GetTestUsersDTO()
        {
            var users = new List<UserDTO>
            {
                new UserDTO { Username = "Musya", Email = "smth.gmail.com", Password = "qwerty",
                    Role = "cat", BirthDate = DateTime.Now.AddYears(-4), Sex = "female"},
                new UserDTO { Username = "Patya", Email = "goddess.gmail.com", Password = "alsdcjhsidcjladj",
                    Role = "cat", BirthDate = DateTime.Now.AddYears(-14), Sex = "female"}
            };
            return users;
        }

        #endregion

        public UserServiceTests()
        {
            _mockUserManager = new Mock<UserManager<User>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>();
            _mockSignInManager = new Mock<SignInManager<User>>();
            _mockMapper = new Mock<IMapper>();
            _mockIdentityResult = new Mock<IdentityResult>();
        }

        [SetUp]
        public void Setup()
        {
            _service = new UserService(_mockUserManager.Object, _mockRoleManager.Object, _mockSignInManager.Object, _mockMapper.Object);
        }

        #region Register
        [Test]
        public void Register_Valid()
        {
            _mockUserManager.Setup(um => um.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<User>());
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<IdentityResult>());
            _mockIdentityResult.Setup(ir => ir.Succeeded).Returns(true);

            var result = _service.Register(GetTestUsersDTO().FirstOrDefault());

            Assert.That(result, Is.True);
            _mockUserManager.Verify();
            _mockIdentityResult.Verify();

        }
        #endregion

        #region Login
        #endregion

        #region Logout
        #endregion
    }
}
