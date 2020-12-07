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
using Theater.Domain.Core.Models.Actor;
using Theater.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Infrastructure.Business.UnitTests.Actors
{
    class ActorControllerTests
    {
        #region Members
        private ActorsController _controller;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBaseService<ActorDTO>> _mockService;
        private readonly Mock<ILogger<ActorsController>> _mockLogger;

        private List<ActorDTO> GetTestActorsDTO()
        {
            var actors = new List<ActorDTO>
            {
                new ActorDTO { Id = 1, EyeColor = "1", HairColor = "1", Nationality = "1", Height = 160, UserId = "qwaszx"},
                new ActorDTO { Id = 2, EyeColor = "2", HairColor = "2", Nationality = "2", Height = 170, UserId = "erdfcv"}
            };
            return actors;
        }

        private List<CreateActorModel> GetTestCreateActors()
        {
            var actors = new List<CreateActorModel>
            {
                new CreateActorModel { EyeColor = "1", HairColor = "1", Nationality = "1", Height = 160, UserId = "qwaszx"},
                new CreateActorModel { EyeColor = "2", HairColor = "2", Nationality = "2", Height = 170, UserId = "erdfcv"}
            };
            return actors;
        }

        private List<UpdateActorModel> GetTestUpdateActors()
        {
            var actors = new List<UpdateActorModel>
            {
                new UpdateActorModel { Id = 1, EyeColor = "1", HairColor = "1", Nationality = "1", Height = 160},
                new UpdateActorModel { Id = 2, EyeColor = "2", HairColor = "2", Nationality = "2", Height = 170}
            };
            return actors;
        }

        private static int getTestActorId = 1;
        #endregion

        public ActorControllerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockService = new Mock<IBaseService<ActorDTO>>();
            _mockLogger = new Mock<ILogger<ActorsController>>();
        }

        [SetUp]
        public void Setup()
        {
            _controller = new ActorsController(_mockService.Object, _mockLogger.Object, _mockMapper.Object);
        }

        #region GetItems
        [Test]
        public async Task GetItems_Valid()
        {
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(GetTestActorsDTO());

            var result = await _controller.GetAsync();

            Assert.IsInstanceOf<OkObjectResult>(result);
            _mockService.Verify();
        }

        [Test]
        public async Task GetItems_NullReturned()
        {
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(null as IEnumerable<ActorDTO>);

            var result = await _controller.GetAsync();

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion

        #region GetItem
        [Test]
        public async Task GetItem_Valid()
        {
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new ActorDTO());

            var result = await _controller.GetAsync(getTestActorId);

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
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(null as ActorDTO);

            var result = await _controller.GetAsync(getTestActorId);

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion

        #region Create
        [Test]
        public async Task CreateItem_Valid()
        {
            _mockMapper.Setup(m => m.Map<ActorDTO>(It.IsAny<CreateActorModel>()))
                .Returns(new ActorDTO());
            _mockService.Setup(s => s.CreateAsync(It.IsAny<ActorDTO>()))
                .ReturnsAsync(true);

            var result = await _controller.PostAsync(GetTestCreateActors().FirstOrDefault());

            Assert.IsInstanceOf<OkResult>(result);
            _mockService.Verify();
            _mockMapper.Verify();
        }
        [Test]
        public async Task CreateItem_InvalidModel()
        {
            _controller.ModelState.AddModelError("error", "invalidModel");

            var result = await _controller.PostAsync(GetTestCreateActors().FirstOrDefault());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region Update
        [Test]
        public async Task Update_Valid()
        {
            _mockMapper.Setup(m => m.Map<ActorDTO>(It.IsAny<UpdateActorModel>()))
                .Returns(new ActorDTO());
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<ActorDTO>()))
                .ReturnsAsync(true);

            var result = await _controller.UpdateAsync(GetTestUpdateActors().FirstOrDefault());

            Assert.IsInstanceOf<OkResult>(result);
            _mockService.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task Update_InvalidModel()
        {
            _controller.ModelState.AddModelError("error", "invalidModel");

            var result = await _controller.UpdateAsync(GetTestUpdateActors().FirstOrDefault());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Update_FalseReturned()
        {
            _mockMapper.Setup(m => m.Map<ActorDTO>(It.IsAny<UpdateActorModel>()))
                .Returns(new ActorDTO());
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<ActorDTO>()))
                .ReturnsAsync(false);

            var result = await _controller.UpdateAsync(GetTestUpdateActors().FirstOrDefault());

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
            _mockService.Setup(s => s.DeleteAsync(getTestActorId)).ReturnsAsync(false);

            var result = await _controller.DeleteAsync(getTestActorId);

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion
    }
}
