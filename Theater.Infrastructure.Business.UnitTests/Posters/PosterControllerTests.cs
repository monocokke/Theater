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
using Theater.Domain.Core.Models.Poster;
using Theater.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Infrastructure.Business.UnitTests.Posters
{
    class PosterControllerTests
    {
        #region Members
        private PostersController _controller;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBaseService<PosterDTO>> _mockService;
        private readonly Mock<ILogger<PostersController>> _mockLogger;

        private List<PosterDTO> GetTestPostersDTO()
        {
            var posters = new List<PosterDTO>
            {
                new PosterDTO { Id = 1, DateTime = DateTime.Now.AddDays(-2), Premiere = true, PerformanceId = 1},
                new PosterDTO { Id = 2, DateTime = DateTime.Now, Premiere = false, PerformanceId = 1}
            };
            return posters;
        }

        private List<CreatePosterModel> GetTestCreatePosters()
        {
            var posters = new List<CreatePosterModel>
            {
                new CreatePosterModel { DateTime = DateTime.Now.AddDays(-2), Premiere = true, PerformanceId = 1},
                new CreatePosterModel { DateTime = DateTime.Now, Premiere = false, PerformanceId = 1}
            };
            return posters;
        }

        private List<UpdatePosterModel> GetTestUpdatePosters()
        {
            var posters = new List<UpdatePosterModel>
            {
                new UpdatePosterModel { Id = 1, DateTime = DateTime.Now.AddDays(-2), Premiere = true, PerformanceId = 1},
                new UpdatePosterModel { Id = 2, DateTime = DateTime.Now, Premiere = false, PerformanceId = 1}
            };
            return posters;
        }

        private static int getTestPosterId = 1;
        #endregion

        public PosterControllerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockService = new Mock<IBaseService<PosterDTO>>();
            _mockLogger = new Mock<ILogger<PostersController>>();
        }

        [SetUp]
        public void Setup()
        {
            _controller = new PostersController(_mockService.Object, _mockLogger.Object, _mockMapper.Object);
        }

        #region GetItems
        [Test]
        public async Task GetItems_Valid()
        {
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(GetTestPostersDTO());

            var result = await _controller.GetAsync();

            Assert.IsInstanceOf<OkObjectResult>(result);
            _mockService.Verify();
        }

        [Test]
        public async Task GetItems_NullReturned()
        {
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync((IEnumerable<PosterDTO>)null);

            var result = await _controller.GetAsync();

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion

        #region GetItem
        [Test]
        public async Task GetItem_Valid()
        {
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new PosterDTO());

            var result = await _controller.GetAsync(getTestPosterId);

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
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(null as PosterDTO);

            var result = await _controller.GetAsync(getTestPosterId);

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion

        #region Create
        [Test]
        public async Task CreateItem_Valid()
        {
            _mockMapper.Setup(m => m.Map<PosterDTO>(It.IsAny<CreatePosterModel>()))
                .Returns(new PosterDTO());
            _mockService.Setup(s => s.CreateAsync(It.IsAny<PosterDTO>()))
                .ReturnsAsync(true);

            var result = await _controller.PostAsync(GetTestCreatePosters().FirstOrDefault());

            Assert.IsInstanceOf<OkResult>(result);
            _mockService.Verify();
            _mockMapper.Verify();
        }
        [Test]
        public async Task CreateItem_InvalidModel()
        {
            _controller.ModelState.AddModelError("error", "invalidModel");

            var result = await _controller.PostAsync(GetTestCreatePosters().FirstOrDefault());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region Update
        [Test]
        public async Task Update_Valid()
        {
            _mockMapper.Setup(m => m.Map<PosterDTO>(It.IsAny<UpdatePosterModel>()))
                .Returns(new PosterDTO());
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<PosterDTO>()))
                .ReturnsAsync(true);

            var result = await _controller.UpdateAsync(GetTestUpdatePosters().FirstOrDefault());

            Assert.IsInstanceOf<OkResult>(result);
            _mockService.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task Update_InvalidModel()
        {
            _controller.ModelState.AddModelError("error", "invalidModel");

            var result = await _controller.UpdateAsync(GetTestUpdatePosters().FirstOrDefault());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Update_FalseReturned()
        {
            _mockMapper.Setup(m => m.Map<PosterDTO>(It.IsAny<UpdatePosterModel>()))
                .Returns(new PosterDTO());
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<PosterDTO>()))
                .ReturnsAsync(false);

            var result = await _controller.UpdateAsync(GetTestUpdatePosters().FirstOrDefault());

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
            _mockService.Setup(s => s.DeleteAsync(getTestPosterId)).ReturnsAsync(false);

            var result = await _controller.DeleteAsync(getTestPosterId);

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion
    }
}
