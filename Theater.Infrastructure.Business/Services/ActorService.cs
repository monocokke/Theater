using AutoMapper;
using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;

namespace Theater.Infrastructure.Business.Services
{
    public class ActorService : IService<ActorDTO>
    {
        private readonly IRepository<Actor> _actors;
        private readonly IMapper _mapper;

        public ActorService(IRepository<Actor> actorRepository, IMapper mapper)
        {
            _actors = actorRepository;
            _mapper = mapper;
        }
        public bool CreateItem(ActorDTO actorDTO)
        {
            _actors.Create(_mapper.Map<Actor>(actorDTO));
            return true;
        }

        public ActorDTO GetItem(int id)
        {
            return _mapper.Map<ActorDTO>(_actors.Get(id));
        }

        public IEnumerable<ActorDTO> GetItems()
        {
            return _mapper.Map<IEnumerable<ActorDTO>>(_actors.GetList());
        }

        public bool Update(ActorDTO actorDTO)
        {
            var edited = _actors.Get(actorDTO.Id);
            if (edited == null)
                return false;
            if (actorDTO.EyeColor != null) { edited.EyeColor = actorDTO.EyeColor; }
            if (actorDTO.HairColor != null) { edited.HairColor = actorDTO.HairColor; }
            if (actorDTO.Nationality != null) { edited.Nationality = actorDTO.Nationality; }
            if (actorDTO.Height != null) { edited.Height = actorDTO.Height; }
            _actors.Update(_mapper.Map<Actor>(actorDTO));
            return true;
        }

        public bool Delete(int id)
        {
            if (id > 0 && _actors.Get(id) != null)
            {
                _actors.Delete(id);
            }
            return true;
        }
    }
}
