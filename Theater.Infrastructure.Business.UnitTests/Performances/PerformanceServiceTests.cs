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
using System.Threading.Tasks;

namespace Theater.Infrastructure.Business.UnitTests
{
    public class PerformanceServiceTests
    {
        #region Members
        private IBaseService<PerformanceDTO> _service;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBaseRepository<Performance>> _mockPerformanceRepository;

        private List<PerformanceDTO> GetTestPerformancesDTO()
        {
            var performances = new List<PerformanceDTO>
            {
                new PerformanceDTO { Id = 1, Name = "Breaks", Genre = "comedy", Audience = "family"},
                new PerformanceDTO { Id = 2, Name = "Dreaming", Genre = "drama", Audience = "adult"}
            };
            return performances;
        }

        private static int getTestPerformanceId = 1;
        #endregion

        public PerformanceServiceTests()
        {
            _mockPerformanceRepository = new Mock<IBaseRepository<Performance>>();
            _mockMapper = new Mock<IMapper>();
        }

        [SetUp]
        public void Setup()
        {
            _service = new PerformanceService(_mockPerformanceRepository.Object, _mockMapper.Object);
        }

        #region GetItem
        [Test]
        public async Task GetItem_ValidAsync()
        {
            _mockPerformanceRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Performance>());
            _mockMapper.Setup(m => m.Map<PerformanceDTO>(It.IsAny<Performance>()))
                .Returns(new PerformanceDTO());

            var result = await _service.GetByIdAsync(getTestPerformanceId);

            _mockPerformanceRepository.Verify();
            _mockMapper.Verify();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetItem_NullReturnedAsync()
        {
            _mockPerformanceRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Performance);

            var result = await _service.GetByIdAsync(getTestPerformanceId);

            Assert.IsNull(result);
            _mockPerformanceRepository.Verify();
        }
        #endregion

        #region GetItems
        [Test]
        public async Task GetItems_ValidAsync()
        {
            _mockPerformanceRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(It.IsAny<IEnumerable<Performance>>());
            _mockMapper.Setup(m => m.Map<IEnumerable<PerformanceDTO>>(It.IsAny<IEnumerable<Performance>>()))
                .Returns(GetTestPerformancesDTO());

            var result = await _service.GetAllAsync();

            Assert.IsNotNull(result);
            _mockPerformanceRepository.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task GetItems_NullReturnedAsync()
        {
            _mockPerformanceRepository.Setup(r => r.GetAllAsync()).ReturnsAsync((IEnumerable<Performance>)null);

            var result = await _service.GetAllAsync();

            Assert.IsNull(result);
        }
        #endregion

        #region CreateItem
        [Test]
        public async Task CreateItem_ValidAsync()
        {
            _mockPerformanceRepository.Setup(r => r.CreateAsync(It.IsAny<Performance>()));
            _mockMapper.Setup(m => m.Map<Performance>(It.IsAny<PerformanceDTO>()))
                .Returns(new Performance());

            var result = await _service.CreateAsync(GetTestPerformancesDTO().FirstOrDefault());

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
        public async Task UpdateItem_ValidAsync()
        {
            _mockPerformanceRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Performance());
            _mockMapper.Setup(m => m.Map<Performance>(It.IsAny<PerformanceDTO>()))
                .Returns(new Performance());
            _mockPerformanceRepository.Setup(r => r.UpdateAsync(It.IsAny<Performance>()));

            var result = await _service.UpdateAsync(GetTestPerformancesDTO().FirstOrDefault());

            Assert.IsTrue(result);
            _mockPerformanceRepository.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task UpdateItem_ItemNotFoundAsync()
        {
            _mockPerformanceRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Performance);

            var result = await _service.UpdateAsync(GetTestPerformancesDTO().FirstOrDefault());

            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateItem_NullReferencedAsync()
        {
            var result = await _service.UpdateAsync(null);

            Assert.IsFalse(result);
        }
        #endregion

        #region Delete
        [Test]
        public async Task DeleteItem_ValidAsync()
        {
            _mockPerformanceRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Performance());
            _mockPerformanceRepository.Setup(r => r.DeleteAsync(It.IsAny<Performance>()));

            var result = await _service.DeleteAsync(getTestPerformanceId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteItem_ItemNotFoundAsync()
        {
            _mockPerformanceRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Performance);

            var result = await _service.DeleteAsync(It.IsAny<int>());

            Assert.IsFalse(result);
        }
        #endregion
    }
}