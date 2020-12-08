using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;

namespace Theater.Infrastructure.Business.Services
{
    public class ActorService : IBaseService<ActorDTO>
    {
        private readonly IBaseRepository<Actor> _baseRepository;
        private readonly IMapper _mapper;

        public ActorService(IBaseRepository<Actor> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(ActorDTO dto)
        {
            if (dto == null)
                return false;
            await _baseRepository.CreateAsync(_mapper.Map<Actor>(dto));
            return true;
        }

        public async Task<ActorDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<ActorDTO>(await _baseRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<ActorDTO>> GetAllAsync()
        {
            var items = _mapper.Map<IEnumerable<ActorDTO>>(await _baseRepository.GetAllAsync());
            if (!items.Any())
                return null;
            return items;
        }

        public async Task<bool> UpdateAsync(ActorDTO dto)
        {
            if (dto == null)
                return false;
            var item = await _baseRepository.GetByIdAsync(dto.Id);
            if (item == null)
                return false;
            item.EyeColor = dto.EyeColor;
            item.HairColor = dto.HairColor;
            item.Nationality = dto.Nationality;
            item.Height = dto.Height;
            await _baseRepository.UpdateAsync(item);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = _baseRepository.GetByIdAsync(id).Result;
            if (item != null)
            {
                await _baseRepository.DeleteAsync(item);
                return true;
            }
            return false;
        }
    }
}
