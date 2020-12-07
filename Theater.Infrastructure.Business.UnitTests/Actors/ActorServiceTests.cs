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

namespace Theater.Infrastructure.Business.UnitTests.Actors
{
    class ActorServiceTests
    {
        #region Members
        private IBaseService<ActorDTO> _service;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBaseRepository<Actor>> _mockActorRepository;

        private List<ActorDTO> GetTestActorsDTO()
        {
            var actors = new List<ActorDTO>
            {
                new ActorDTO { Id = 1, EyeColor = "1", HairColor = "1", Nationality = "1", Height = 160, UserId = "qwaszx"},
                new ActorDTO { Id = 2, EyeColor = "2", HairColor = "2", Nationality = "2", Height = 170, UserId = "erdfcv"}
            };
            return actors;
        }

        private static int getTestActorId = 1;
        #endregion

        public ActorServiceTests()
        {
            _mockActorRepository = new Mock<IBaseRepository<Actor>>();
            _mockMapper = new Mock<IMapper>();
        }

        [SetUp]
        public void Setup()
        {
            _service = new ActorService(_mockActorRepository.Object, _mockMapper.Object);
        }

        #region GetItem
        [Test]
        public async Task GetItem_Valid()
        {
            _mockActorRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Actor>());
            _mockMapper.Setup(m => m.Map<ActorDTO>(It.IsAny<Actor>()))
                .Returns(new ActorDTO());

            var result = await _service.GetByIdAsync(getTestActorId);

            _mockActorRepository.Verify();
            _mockMapper.Verify();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetItem_NullReturned()
        {
            _mockActorRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Actor);

            var result = await _service.GetByIdAsync(getTestActorId);

            Assert.IsNull(result);
            _mockActorRepository.Verify();
        }
        #endregion

        #region GetItems
        [Test]
        public async Task GetItems_Valid()
        {
            _mockActorRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(It.IsAny<IEnumerable<Actor>>());
            _mockMapper.Setup(m => m.Map<IEnumerable<ActorDTO>>(It.IsAny<IEnumerable<Actor>>()))
                .Returns(GetTestActorsDTO());

            var result = await _service.GetAllAsync();

            Assert.IsNotNull(result);
            _mockActorRepository.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task GetItems_NullReturned()
        {
            _mockActorRepository.Setup(r => r.GetAllAsync()).ReturnsAsync((IEnumerable<Actor>)null);

            var result = await _service.GetAllAsync();

            Assert.IsNull(result);
        }
        #endregion

        #region CreateItem
        [Test]
        public async Task CreateItem_Valid()
        {
            _mockActorRepository.Setup(r => r.CreateAsync(It.IsAny<Actor>()));
            _mockMapper.Setup(m => m.Map<Actor>(It.IsAny<ActorDTO>()))
                .Returns(new Actor());

            var result = await _service.CreateAsync(GetTestActorsDTO().FirstOrDefault());

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
            _mockActorRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Actor());
            _mockMapper.Setup(m => m.Map<Actor>(It.IsAny<ActorDTO>()))
                .Returns(new Actor());
            _mockActorRepository.Setup(r => r.UpdateAsync(It.IsAny<Actor>()));

            var result = await _service.UpdateAsync(GetTestActorsDTO().FirstOrDefault());

            Assert.IsTrue(result);
            _mockActorRepository.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task UpdateItem_ItemNotFound()
        {
            _mockActorRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Actor);

            var result = await _service.UpdateAsync(GetTestActorsDTO().FirstOrDefault());

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
            _mockActorRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Actor());
            _mockActorRepository.Setup(r => r.DeleteAsync(It.IsAny<Actor>()));

            var result = await _service.DeleteAsync(getTestActorId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteItem_ItemNotFound()
        {
            _mockActorRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Actor);

            var result = await _service.DeleteAsync(It.IsAny<int>());

            Assert.IsFalse(result);
        }
        #endregion
    }
}
