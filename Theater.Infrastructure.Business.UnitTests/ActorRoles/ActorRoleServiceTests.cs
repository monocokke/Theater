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

namespace Theater.Infrastructure.Business.UnitTests.ActorRoles
{
    class ActorRoleServiceTests
    {
        #region Members
        private IBaseService<ActorRoleDTO> _service;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBaseRepository<ActorRole>> _mockActorRoleRepository;

        private List<ActorRoleDTO> GetTestActorRolesDTO()
        {
            var actorRoles = new List<ActorRoleDTO>
            {
                new ActorRoleDTO { Id = 1, ActorId = 1, RoleId = 1, Understudy = true},
                new ActorRoleDTO { Id = 2, ActorId = 2, RoleId = 1, Understudy = false}
            };
            return actorRoles;
        }

        private static int getTestActorRoleId = 1;
        #endregion

        public ActorRoleServiceTests()
        {
            _mockActorRoleRepository = new Mock<IBaseRepository<ActorRole>>();
            _mockMapper = new Mock<IMapper>();
        }

        [SetUp]
        public void Setup()
        {
            _service = new ActorRoleService(_mockActorRoleRepository.Object, _mockMapper.Object);
        }

        #region GetItem
        [Test]
        public async Task GetItem_ValidAsync()
        {
            _mockActorRoleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<ActorRole>());
            _mockMapper.Setup(m => m.Map<ActorRoleDTO>(It.IsAny<ActorRole>()))
                .Returns(new ActorRoleDTO());

            var result = await _service.GetByIdAsync(getTestActorRoleId);

            _mockActorRoleRepository.Verify();
            _mockMapper.Verify();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetItem_NullReturnedAsync()
        {
            _mockActorRoleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as ActorRole);

            var result = await _service.GetByIdAsync(getTestActorRoleId);

            Assert.IsNull(result);
            _mockActorRoleRepository.Verify();
        }
        #endregion

        #region GetItems
        [Test]
        public async Task GetItems_ValidAsync()
        {
            _mockActorRoleRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(It.IsAny<IEnumerable<ActorRole>>());
            _mockMapper.Setup(m => m.Map<IEnumerable<ActorRoleDTO>>(It.IsAny<IEnumerable<ActorRole>>()))
                .Returns(GetTestActorRolesDTO());

            var result = await _service.GetAllAsync();

            Assert.IsNotNull(result);
            _mockActorRoleRepository.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task GetItems_NullReturnedAsync()
        {
            _mockActorRoleRepository.Setup(r => r.GetAllAsync()).ReturnsAsync((IEnumerable<ActorRole>)null);

            var result = await _service.GetAllAsync();

            Assert.IsNull(result);
        }
        #endregion

        #region CreateItem
        [Test]
        public async Task CreateItem_ValidAsync()
        {
            _mockActorRoleRepository.Setup(r => r.CreateAsync(It.IsAny<ActorRole>()));
            _mockMapper.Setup(m => m.Map<ActorRole>(It.IsAny<ActorRoleDTO>()))
                .Returns(new ActorRole());

            var result = await _service.CreateAsync(GetTestActorRolesDTO().FirstOrDefault());

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateItem_NullReferencedAsync()
        {
            var result = await _service.CreateAsync(null);

            Assert.IsFalse(result);
        }
        #endregion

        #region Update
        [Test]
        public async Task UpdateItem_Valid()
        {
            _mockActorRoleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new ActorRole());
            _mockMapper.Setup(m => m.Map<ActorRole>(It.IsAny<ActorRoleDTO>()))
                .Returns(new ActorRole());
            _mockActorRoleRepository.Setup(r => r.UpdateAsync(It.IsAny<ActorRole>()));

            var result = await _service.UpdateAsync(GetTestActorRolesDTO().FirstOrDefault());

            Assert.IsTrue(result);
            _mockActorRoleRepository.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task UpdateItem_ItemNotFound()
        {
            _mockActorRoleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as ActorRole);

            var result = await _service.UpdateAsync(GetTestActorRolesDTO().FirstOrDefault());

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
            _mockActorRoleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new ActorRole());
            _mockActorRoleRepository.Setup(r => r.DeleteAsync(It.IsAny<ActorRole>()));

            var result = await _service.DeleteAsync(getTestActorRoleId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteItem_ItemNotFound()
        {
            _mockActorRoleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as ActorRole);

            var result = await _service.DeleteAsync(It.IsAny<int>());

            Assert.IsFalse(result);
        }
        #endregion
    }
}
