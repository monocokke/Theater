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
using Theater.Domain.Core.Models.ActorRole;
using Theater.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Infrastructure.Business.UnitTests.ActorRoles
{
    class ActorRoleControllerTests
    {
        #region Members
        private ActorRolesController _controller;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBaseService<ActorRoleDTO>> _mockService;
        private readonly Mock<ILogger<ActorRolesController>> _mockLogger;

        private List<ActorRoleDTO> GetTestActorRolesDTO()
        {
            var actorRoles = new List<ActorRoleDTO>
            {
                new ActorRoleDTO { Id = 1, ActorId = 1, RoleId = 1, Understudy = true},
                new ActorRoleDTO { Id = 2, ActorId = 2, RoleId = 1, Understudy = false}
            };
            return actorRoles;
        }

        private List<CreateActorRoleModel> GetTestCreateActorRoles()
        {
            var actorRoles = new List<CreateActorRoleModel>
            {
                new CreateActorRoleModel { ActorId = 1, RoleId = 1, Understudy = true},
                new CreateActorRoleModel { ActorId = 2, RoleId = 1, Understudy = false}
            };
            return actorRoles;
        }

        private List<UpdateActorRoleModel> GetTestUpdateActorRoles()
        {
            var actorRoles = new List<UpdateActorRoleModel>
            {
                new UpdateActorRoleModel { Id = 1, ActorId = 1, RoleId = 1, Understudy = true},
                new UpdateActorRoleModel { Id = 2, ActorId = 2, RoleId = 1, Understudy = false}
            };
            return actorRoles;
        }

        private static int getTestActorRoleId = 1;
        #endregion

        public ActorRoleControllerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockService = new Mock<IBaseService<ActorRoleDTO>>();
            _mockLogger = new Mock<ILogger<ActorRolesController>>();
        }

        [SetUp]
        public void Setup()
        {
            _controller = new ActorRolesController(_mockService.Object, _mockLogger.Object, _mockMapper.Object);
        }

        #region GetAllAsync
        [Test]
        public async Task GetAllAsync_ValidAsync()
        {
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(GetTestActorRolesDTO());

            var result = await _controller.GetAsync();

            Assert.IsInstanceOf<OkObjectResult>(result);
            _mockService.Verify();
        }

        [Test]
        public async Task GetAllAsync_NullReturnedAsync()
        {
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(null as IEnumerable<ActorRoleDTO>);

            var result = await _controller.GetAsync();

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion

        #region GetItem
        [Test]
        public async Task GetItem_ValidAsync()
        {
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new ActorRoleDTO());

            var result = await _controller.GetAsync(getTestActorRoleId);

            Assert.IsInstanceOf<OkObjectResult>(result);
            _mockService.Verify();
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task GetItem_InvalidIdAsync(int id)
        {
            var result = await _controller.GetAsync(id);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task GetItem_NullReturnedAsync()
        {
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(null as ActorRoleDTO);

            var result = await _controller.GetAsync(getTestActorRoleId);

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion

        #region Create
        [Test]
        public async Task CreateItem_ValidAsync()
        {
            _mockMapper.Setup(m => m.Map<ActorRoleDTO>(It.IsAny<CreateActorRoleModel>()))
                .Returns(new ActorRoleDTO());
            _mockService.Setup(s => s.CreateAsync(It.IsAny<ActorRoleDTO>()))
                .ReturnsAsync(true);

            var result = await _controller.PostAsync(GetTestCreateActorRoles().FirstOrDefault());

            Assert.IsInstanceOf<OkResult>(result);
            _mockService.Verify();
            _mockMapper.Verify();
        }
        [Test]
        public async Task CreateItem_InvalidModelAsync()
        {
            _controller.ModelState.AddModelError("error", "invalidModel");

            var result = await _controller.PostAsync(GetTestCreateActorRoles().FirstOrDefault());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region Update
        [Test]
        public async Task Update_ValidAsync()
        {
            _mockMapper.Setup(m => m.Map<ActorRoleDTO>(It.IsAny<UpdateActorRoleModel>()))
                .Returns(new ActorRoleDTO());
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<ActorRoleDTO>()))
                .ReturnsAsync(true);

            var result = await _controller.UpdateAsync(GetTestUpdateActorRoles().FirstOrDefault());

            Assert.IsInstanceOf<OkResult>(result);
            _mockService.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task Update_InvalidModelAsync()
        {
            _controller.ModelState.AddModelError("error", "invalidModel");

            var result = await _controller.UpdateAsync(GetTestUpdateActorRoles().FirstOrDefault());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Update_FalseReturnedAsync()
        {
            _mockMapper.Setup(m => m.Map<ActorRoleDTO>(It.IsAny<UpdateActorRoleModel>()))
                .Returns(new ActorRoleDTO());
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<ActorRoleDTO>()))
                .ReturnsAsync(false);

            var result = await  _controller.UpdateAsync(GetTestUpdateActorRoles().FirstOrDefault());

            Assert.IsInstanceOf<BadRequestResult>(result);
            _mockService.Verify();
            _mockMapper.Verify();
        }
        #endregion

        #region Delete
        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task Delete_InvalidIdAsync(int id)
        {
            var result = await _controller.DeleteAsync(id);

            Assert.IsInstanceOf<BadRequestResult>(result);
        }

        [Test]
        public async Task Delete_ItemNotFoundAsync()
        {
            _mockService.Setup(s => s.DeleteAsync(getTestActorRoleId)).ReturnsAsync(false);

            var result = await _controller.DeleteAsync(getTestActorRoleId);

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion
    }
}
