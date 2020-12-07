using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Theater.Controllers;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Models.Role;
using Theater.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Infrastructure.Business.UnitTests.Roles
{
    class RoleControllerTests
    {
        #region Members
        private RolesController _controller;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBaseService<RoleDTO>> _mockService;
        private readonly Mock<ILogger<RolesController>> _mockLogger;

        private List<RoleDTO> GetTestRolesDTO()
        {
            var roles = new List<RoleDTO>
            {
                new RoleDTO { Id = 1, Name = "CommonName", Age = 18, Sex = "male", EyeColor =  "1",
                                HairColor = "1", Nationality = "1", Height = 160, Description = "Good man",
                                    PerformanceId = 1},
                new RoleDTO { Id = 2, Name = "RareName", Age = 20, Sex = "female", EyeColor =  "2",
                                HairColor = "2", Nationality = "2", Height = 170, Description = "Good women",
                                    PerformanceId = 2}
            };
            return roles;
        }

        private List<CreateRoleModel> GetTestCreateRoles()
        {
            var roles = new List<CreateRoleModel>
            {
                new CreateRoleModel { Name = "CommonName", Age = 18, Sex = "male", EyeColor =  "1",
                                HairColor = "1", Nationality = "1", Height = 160, Description = "Good man",
                                    PerformanceId = 1},
                new CreateRoleModel { Name = "RareName", Age = 20, Sex = "female", EyeColor =  "2",
                                HairColor = "2", Nationality = "2", Height = 170, Description = "Good women",
                                    PerformanceId = 2}
            };
            return roles;
        }

        private List<UpdateRoleModel> GetTestUpdateRoles()
        {
            var roles = new List<UpdateRoleModel>
            {
                new UpdateRoleModel { Id = 1, Name = "CommonName", Age = 18, Sex = "male", EyeColor =  "1",
                                HairColor = "1", Nationality = "1", Height = 160, Description = "Good man",
                                    PerformanceId = 1},
                new UpdateRoleModel { Id = 2, Name = "RareName", Age = 20, Sex = "female", EyeColor =  "2",
                                HairColor = "2", Nationality = "2", Height = 170, Description = "Good women",
                                    PerformanceId = 2}
            };
            return roles;
        }

        private static int getTestRoleId = 1;
        #endregion

        public RoleControllerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockService = new Mock<IBaseService<RoleDTO>>();
            _mockLogger = new Mock<ILogger<RolesController>>();
        }

        [SetUp]
        public void Setup()
        {
            _controller = new RolesController(_mockService.Object, _mockLogger.Object, _mockMapper.Object);
        }

        #region GetItems
        [Test]
        public async Task GetItems_Valid()
        {
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(GetTestRolesDTO());

            var result = await _controller.GetAsync();

            Assert.IsInstanceOf<OkObjectResult>(result);
            _mockService.Verify();
        }

        [Test]
        public async Task GetItems_NullReturned()
        {
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(null as IEnumerable<RoleDTO>);

            var result = await _controller.GetAsync();

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion

        #region GetItem
        [Test]
        public async Task GetItem_Valid()
        {
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new RoleDTO());

            var result = await _controller.GetAsync(getTestRoleId);

            Assert.IsInstanceOf<OkObjectResult>(result);
            _mockService.Verify();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task GetItem_InvalidId(int id)
        {
            var result = await _controller.GetAsync(id);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task GetItem_NullReturned()
        {
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(null as RoleDTO);

            var result = await  _controller.GetAsync(getTestRoleId);

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion

        #region Create
        [Test]
        public async Task CreateItem_Valid()
        {
            _mockMapper.Setup(m => m.Map<RoleDTO>(It.IsAny<CreateRoleModel>()))
                .Returns(new RoleDTO());
            _mockService.Setup(s => s.CreateAsync(It.IsAny<RoleDTO>()))
                .ReturnsAsync(true);

            var result = await _controller.PostAsync(GetTestCreateRoles().FirstOrDefault());

            Assert.IsInstanceOf<OkResult>(result);
            _mockService.Verify();
            _mockMapper.Verify();
        }
        [Test]
        public async Task CreateItem_InvalidModel()
        {
            _controller.ModelState.AddModelError("error", "invalidModel");

            var result = await _controller.PostAsync(GetTestCreateRoles().FirstOrDefault());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region Update
        [Test]
        public async Task Update_Valid()
        {
            _mockMapper.Setup(m => m.Map<RoleDTO>(It.IsAny<UpdateRoleModel>()))
                .Returns(new RoleDTO());
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<RoleDTO>()))
                .ReturnsAsync(true);

            var result = await _controller.UpdateAsync(GetTestUpdateRoles().FirstOrDefault());

            Assert.IsInstanceOf<OkResult>(result);
            _mockService.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task Update_InvalidModel()
        {
            _controller.ModelState.AddModelError("error", "invalidModel");

            var result = await _controller.UpdateAsync(GetTestUpdateRoles().FirstOrDefault());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Update_FalseReturned()
        {
            _mockMapper.Setup(m => m.Map<RoleDTO>(It.IsAny<UpdateRoleModel>()))
                .Returns(new RoleDTO());
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<RoleDTO>()))
                .ReturnsAsync(false);

            var result = await _controller.UpdateAsync(GetTestUpdateRoles().FirstOrDefault());

            Assert.IsInstanceOf<BadRequestResult>(result);
            _mockService.Verify();
            _mockMapper.Verify();
        }
        #endregion

        #region Delete
        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task Delete_InvalidId(int id)
        {
            var result = await _controller.DeleteAsync(id);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task Delete_ItemNotFound()
        {
            _mockService.Setup(s => s.DeleteAsync(getTestRoleId)).ReturnsAsync(false);

            var result = await _controller.DeleteAsync(getTestRoleId);

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion
    }
}
