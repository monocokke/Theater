using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;

namespace Theater.Infrastructure.Business.Services
{
    public class ActorRoleService : IService<ActorRoleDTO>
    {
        private readonly IRepository<ActorRole> _actorRoles;
        private readonly IMapper _mapper;

        public ActorRoleService(IRepository<ActorRole> actorRoleRepository, IMapper mapper)
        {
            _actorRoles = actorRoleRepository;
            _mapper = mapper;
        }
        public bool CreateItem(ActorRoleDTO actorRoleDTO)
        {
            _actorRoles.Create(_mapper.Map<ActorRole>(actorRoleDTO));
            return true;
        }

        public ActorRoleDTO GetItem(int id)
        {
            return _mapper.Map<ActorRoleDTO>(_actorRoles.Get(id));
        }

        public IEnumerable<ActorRoleDTO> GetItems()
        {
            return _mapper.Map<IEnumerable<ActorRoleDTO>>(_actorRoles.GetList());
        }

        public bool Update(ActorRoleDTO actorRoleDTO)
        {
            var edited = _actorRoles.Get(actorRoleDTO.Id);
            if (edited == null)
                return false;
            if (actorRoleDTO.ActorId != null) { edited.ActorId = actorRoleDTO.ActorId; }
            if (actorRoleDTO.RoleId != null) { edited.RoleId = actorRoleDTO.RoleId; }
            if (actorRoleDTO.Understudy != null) { edited.Understudy = actorRoleDTO.Understudy; }
            _actorRoles.Update(_mapper.Map<ActorRole>(actorRoleDTO));
            return true;
        }

        public bool Delete(int id)
        {
            if (id > 0 && _actorRoles.Get(id) != null)
            {
                _actorRoles.Delete(id);
            }
            return true;
        }
    }
}
