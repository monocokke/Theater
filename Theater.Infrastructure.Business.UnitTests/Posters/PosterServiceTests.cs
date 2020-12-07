using NUnit.Framework;
using System.Collections.Generic;
using Theater.Domain.Core.Entities;
using Theater.Services.Interfaces;
using Moq;
using Theater.Domain.Interfaces;
using Theater.Domain.Core.DTO;
using AutoMapper;
using Theater.Infrastructure.Business.Services;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Theater.Infrastructure.Business.UnitTests.Posters
{
    class PosterServiceTests
    {
        #region Members
        private IBaseService<PosterDTO> _service;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBaseRepository<Poster>> _mockPosterRepository;

        private List<PosterDTO> GetTestPostersDTO()
        {
            var posters = new List<PosterDTO>
            {
                new PosterDTO { Id = 1, DateTime = DateTime.Now.AddDays(-2), Premiere = true, PerformanceId = 1},
                new PosterDTO { Id = 2, DateTime = DateTime.Now, Premiere = false, PerformanceId = 1}
            };
            return posters;
        }

        private static int getTestPosterId = 1;
        #endregion

        public PosterServiceTests()
        {
            _mockPosterRepository = new Mock<IBaseRepository<Poster>>();
            _mockMapper = new Mock<IMapper>();
        }

        [SetUp]
        public void Setup()
        {
            _service = new PosterService(_mockPosterRepository.Object, _mockMapper.Object);
        }

        #region GetItem
        [Test]
        public async Task GetItem_Valid()
        {
            _mockPosterRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Poster>());
            _mockMapper.Setup(m => m.Map<PosterDTO>(It.IsAny<Poster>()))
                .Returns(new PosterDTO());

            var result = await _service.GetByIdAsync(getTestPosterId);

            _mockPosterRepository.Verify();
            _mockMapper.Verify();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetItem_NullReturned()
        {
            _mockPosterRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Poster);

            var result = await _service.GetByIdAsync(getTestPosterId);

            Assert.IsNull(result);
            _mockPosterRepository.Verify();
        }
        #endregion

        #region GetItems
        [Test]
        public async Task GetItems_Valid()
        {
            _mockPosterRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(It.IsAny<IEnumerable<Poster>>());
            _mockMapper.Setup(m => m.Map<IEnumerable<PosterDTO>>(It.IsAny<IEnumerable<Poster>>()))
                .Returns(GetTestPostersDTO());

            var result = await _service.GetAllAsync();

            Assert.IsNotNull(result);
            _mockPosterRepository.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task GetItems_NullReturned()
        {
            _mockPosterRepository.Setup(r => r.GetAllAsync()).ReturnsAsync((IEnumerable<Poster>)null);

            var result = await _service.GetAllAsync();

            Assert.IsNull(result);
        }
        #endregion

        #region CreateItem
        [Test]
        public async Task CreateItem_Valid()
        {
            _mockPosterRepository.Setup(r => r.CreateAsync((It.IsAny<Poster>())));
            _mockMapper.Setup(m => m.Map<Poster>(It.IsAny<PosterDTO>()))
                .Returns(new Poster());

            var result = await _service.CreateAsync(GetTestPostersDTO().FirstOrDefault());

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateItem_NullReferenced()
        {
            var result = await _service.CreateAsync(null);

            Assert.IsFalse(result);
        }
        #endregion

        #region Update
        [Test]
        public async Task UpdateItem_Valid()
        {
            _mockPosterRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Poster());
            _mockMapper.Setup(m => m.Map<Poster>(It.IsAny<PosterDTO>()))
                .Returns(new Poster());
            _mockPosterRepository.Setup(r => r.UpdateAsync(It.IsAny<Poster>()));

            var result = await _service.UpdateAsync(GetTestPostersDTO().FirstOrDefault());

            Assert.IsTrue(result);
            _mockPosterRepository.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task UpdateItem_ItemNotFound()
        {
            _mockPosterRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Poster);

            var result = await _service.UpdateAsync(GetTestPostersDTO().FirstOrDefault());

            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateItem_NullReferenced()
        {
            var result = await _service.UpdateAsync(null);

            Assert.IsFalse(result);
        }
        #endregion

        #region Delete
        [Test]
        public async Task DeleteItem_Valid()
        {
            _mockPosterRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Poster());
            _mockPosterRepository.Setup(r => r.DeleteAsync(It.IsAny<Poster>()));

            var result = await _service.DeleteAsync(getTestPosterId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteItem_ItemNotFound()
        {
            _mockPosterRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Poster);

            var result = await _service.DeleteAsync(It.IsAny<int>());

            Assert.IsFalse(result);
        }
        #endregion
    }
}
