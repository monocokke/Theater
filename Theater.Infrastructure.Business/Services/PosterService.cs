using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;
using AutoMapper;

namespace Theater.Infrastructure.Business.Services
{
    public class PosterService : IService<PosterDTO>
    {
        private readonly IRepository<Poster> _posters;
        private readonly IMapper _mapper;

        public PosterService(IRepository<Poster> posterRepository, IMapper mapper)
        {
            _posters = posterRepository;
            _mapper = mapper;
        }
        public bool CreateItem(PosterDTO posterDTO)
        {
            _posters.Create(_mapper.Map<Poster>(posterDTO));
            return true;
        }

        public PosterDTO GetItem(int id)
        {
            return _mapper.Map<PosterDTO>(_posters.Get(id));
        }

        public IEnumerable<PosterDTO> GetItems()
        {
            return _mapper.Map<IEnumerable<PosterDTO>>(_posters.GetList());
        }

        public bool Update(PosterDTO posterDTO)
        {
            var edited = _posters.Get(posterDTO.Id);
            if (edited == null)
                return false;
            if (posterDTO.DateTime != null) { edited.DateTime = posterDTO.DateTime; }
            if (posterDTO.Premiere != null) { edited.Premiere = posterDTO.Premiere; }
            _posters.Update(_mapper.Map<Poster>(posterDTO));
            return true;
        }

        public bool Delete(int id)
        {
            if (id > 0 && _posters.Get(id) != null)
            {
                _posters.Delete(id);
            }
            return true;
        }
    }
}
