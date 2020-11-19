using AutoMapper;
using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;

namespace Theater.Infrastructure.Business.Services
{
    public class RoleService : IService<RoleDTO>
    {
        private readonly IRepository<Role> _roles;
        private readonly IMapper _mapper;

        public RoleService(IRepository<Role> roleRepository, IMapper mapper)
        {
            _roles = roleRepository;
            _mapper = mapper;
        }
        public bool CreateItem(RoleDTO roleDTO)
        {
            _roles.Create(_mapper.Map<Role>(roleDTO));
            return true;
        }

        public RoleDTO GetItem(int id)
        {
            return _mapper.Map<RoleDTO>(_roles.Get(id));
        }

        public IEnumerable<RoleDTO> GetItems()
        {
            return _mapper.Map<IEnumerable<RoleDTO>>(_roles.GetList());
        }

        public bool Update(RoleDTO roleDTO)
        {
            var edited = _roles.Get(roleDTO.Id);
            if (edited == null)
                return false;
            if (roleDTO.Name != null) { edited.Name = roleDTO.Name; }
            if (roleDTO.Age != null) { edited.Age = roleDTO.Age; }
            if (roleDTO.Sex != null) { edited.Sex = roleDTO.Sex; }
            if (roleDTO.EyeColor != null) { edited.EyeColor = roleDTO.EyeColor; }
            if (roleDTO.HairColor != null) { edited.HairColor = roleDTO.HairColor; }
            if (roleDTO.Nationality != null) { edited.Nationality = roleDTO.Nationality; }
            if (roleDTO.Height != null) { edited.Height = roleDTO.Height; }
            if (roleDTO.PerformanceId != null) { edited.PerformanceId = roleDTO.PerformanceId; }
            if (roleDTO.Description != null) { edited.Description = roleDTO.Description; }
            _roles.Update(_mapper.Map<Role>(roleDTO));
            return true;
        }

        public bool Delete(int id)
        {
            if (id > 0 && _roles.Get(id) != null)
            {
                _roles.Delete(id);
            }
            return true;
        }
    }
}
