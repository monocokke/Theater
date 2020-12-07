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
using Theater.Domain.Core.Models.Performance;
using Theater.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Infrastructure.Business.UnitTests.Performances
{
    [TestFixture]
    public class PerformanceControllerTests
    {
        #region Members
        private PerformancesController _controller;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBaseService<PerformanceDTO>> _mockService;
        private readonly Mock<ILogger<PerformancesController>> _mockLogger;

        private List<PerformanceDTO> GetTestPerformancesDTO()
        {
            var performances = new List<PerformanceDTO>
            {
                new PerformanceDTO { Id = 1, Name = "Breaks", Genre = "comedy", Audience = "family" },
                new PerformanceDTO { Id = 2, Name = "Dreaming", Genre = "drama", Audience = "adult"}
            };
            return performances;
        }

        private List<CreatePerformanceModel> GetTestCreatePerformances()
        {
            var performances = new List<CreatePerformanceModel>
            {
                new CreatePerformanceModel { Name = "Breaks", Genre = "comedy", Audience = "family" },
                new CreatePerformanceModel { Name = "Dreaming", Genre = "drama", Audience = "adult"}
            };
            return performances;
        }

        private List<UpdatePerformanceModel> GetTestUpdatePerformances()
        {
            var performances = new List<UpdatePerformanceModel>
            {
                new UpdatePerformanceModel { Id = 1, Name = "Breaks", Genre = "comedy", Audience = "family" },
                new UpdatePerformanceModel { Id = 2, Name = "Dreaming", Genre = "drama", Audience = "adult"}
            };
            return performances;
        }

        private static int getTestPerformanceId = 1;
        #endregion

        public PerformanceControllerTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockService = new Mock<IBaseService<PerformanceDTO>>();
            _mockLogger = new Mock<ILogger<PerformancesController>>();
        }

        [SetUp]
        public void Setup()
        {
            _controller = new PerformancesController(_mockService.Object, _mockLogger.Object, _mockMapper.Object);
        }

        #region GetItems
        [Test]
        public async Task GetItems_ValidAsync()
        {
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(GetTestPerformancesDTO());

            var result = await _controller.GetAsync();

            Assert.IsInstanceOf<OkObjectResult>(result);
            _mockService.Verify();
        }

        [Test]
        public async Task GetItems_NullReturnedAsync()
        {
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(null as IEnumerable<PerformanceDTO>);

            var result = await _controller.GetAsync();

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion

        #region GetItem
        [Test]
        public async Task GetItem_Valid()
        {
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new PerformanceDTO());

            var result = await _controller.GetAsync(getTestPerformanceId);

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
            _mockService.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(null as PerformanceDTO);

            var result = await _controller.GetAsync(getTestPerformanceId);

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion

        #region Create
        [Test]
        public async Task CreateItem_Valid()
        {
            _mockMapper.Setup(m => m.Map<PerformanceDTO>(It.IsAny<CreatePerformanceModel>()))
                .Returns(new PerformanceDTO());
            _mockService.Setup(s => s.CreateAsync(It.IsAny<PerformanceDTO>()))
                .ReturnsAsync(true);

            var result = await _controller.PostAsync(GetTestCreatePerformances().FirstOrDefault());

            Assert.IsInstanceOf<OkResult>(result);
            _mockService.Verify();
            _mockMapper.Verify();
        }
        [Test]
        public async Task CreateItem_InvalidModel()
        {
            _controller.ModelState.AddModelError("error", "invalidModel");

            var result = await _controller.PostAsync(GetTestCreatePerformances().FirstOrDefault());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
        #endregion

        #region Update
        [Test]
        public async Task Update_Valid()
        {
            _mockMapper.Setup(m => m.Map<PerformanceDTO>(It.IsAny<UpdatePerformanceModel>()))
                .Returns(new PerformanceDTO());
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<PerformanceDTO>()))
                .ReturnsAsync(true);

            var result = await _controller.UpdateAsync(GetTestUpdatePerformances().FirstOrDefault());

            Assert.IsInstanceOf<OkResult>(result);
            _mockService.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task Update_InvalidModel()
        {
            _controller.ModelState.AddModelError("error", "invalidModel");

            var result = await _controller.UpdateAsync(GetTestUpdatePerformances().FirstOrDefault());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Update_FalseReturned()
        {
            _mockMapper.Setup(m => m.Map<PerformanceDTO>(It.IsAny<UpdatePerformanceModel>()))
                .Returns(new PerformanceDTO());
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<PerformanceDTO>()))
                .ReturnsAsync(false);

            var result = await _controller.UpdateAsync(GetTestUpdatePerformances().FirstOrDefault());

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
            _mockService.Setup(s => s.DeleteAsync(getTestPerformanceId)).ReturnsAsync(false);

            var result = await _controller.DeleteAsync(getTestPerformanceId);

            Assert.IsInstanceOf<NoContentResult>(result);
            _mockService.Verify();
        }
        #endregion
    }
}
