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

namespace Theater.Infrastructure.Business.UnitTests.Roles
{
    class RoleServiceTests
    {
        #region Members
        private IBaseService<RoleDTO> _service;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IBaseRepository<Role>> _mockRoleRepository;

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

        private static int getTestRoleId = 1;
        #endregion

        public RoleServiceTests()
        {
            _mockRoleRepository = new Mock<IBaseRepository<Role>>();
            _mockMapper = new Mock<IMapper>();
        }

        [SetUp]
        public void Setup()
        {
            _service = new RoleService(_mockRoleRepository.Object, _mockMapper.Object);
        }

        #region GetItem
        [Test]
        public async Task GetItem_Valid()
        {
            _mockRoleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Role>());
            _mockMapper.Setup(m => m.Map<RoleDTO>(It.IsAny<Role>()))
                .Returns(new RoleDTO());

            var result = await _service.GetByIdAsync(getTestRoleId);

            _mockRoleRepository.Verify();
            _mockMapper.Verify();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetItem_NullReturned()
        {
            _mockRoleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Role);

            var result = await _service.GetByIdAsync(getTestRoleId);

            Assert.IsNull(result);
            _mockRoleRepository.Verify();
        }
        #endregion

        #region GetItems
        [Test]
        public async Task GetItems_Valid()
        {
            _mockRoleRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(It.IsAny<IEnumerable<Role>>());
            _mockMapper.Setup(m => m.Map<IEnumerable<RoleDTO>>(It.IsAny<IEnumerable<Role>>()))
                .Returns(GetTestRolesDTO());

            var result = await _service.GetAllAsync();

            Assert.IsNotNull(result);
            _mockRoleRepository.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task GetItems_NullReturned()
        {
            _mockRoleRepository.Setup(r => r.GetAllAsync()).ReturnsAsync((IEnumerable<Role>)null);

            var result = await _service.GetAllAsync();

            Assert.IsNull(result);
        }
        #endregion

        #region CreateItem
        [Test]
        public async Task CreateItem_Valid()
        {
            _mockRoleRepository.Setup(r => r.CreateAsync(It.IsAny<Role>()));
            _mockMapper.Setup(m => m.Map<Role>(It.IsAny<RoleDTO>()))
                .Returns(new Role());

            var result = await _service.CreateAsync(GetTestRolesDTO().FirstOrDefault());

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
            _mockRoleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Role());
            _mockMapper.Setup(m => m.Map<Role>(It.IsAny<RoleDTO>()))
                .Returns(new Role());
            _mockRoleRepository.Setup(r => r.UpdateAsync(It.IsAny<Role>()));

            var result = await _service.UpdateAsync(GetTestRolesDTO().FirstOrDefault());

            Assert.IsTrue(result);
            _mockRoleRepository.Verify();
            _mockMapper.Verify();
        }

        [Test]
        public async Task UpdateItem_ItemNotFound()
        {
            _mockRoleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Role);

            var result = await _service.UpdateAsync(GetTestRolesDTO().FirstOrDefault());

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
            _mockRoleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Role());
            _mockRoleRepository.Setup(r => r.DeleteAsync(It.IsAny<Role>()));

            var result = await _service.DeleteAsync(getTestRoleId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteItem_ItemNotFound()
        {
            _mockRoleRepository.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Role);

            var result = await _service.DeleteAsync(It.IsAny<int>());

            Assert.IsFalse(result);
        }
        #endregion
    }
}
