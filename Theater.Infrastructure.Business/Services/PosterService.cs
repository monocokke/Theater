using System.Collections.Generic;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Infrastructure.Business.Services
{
    public class PosterService : IBaseService<PosterDTO>
    {
        private readonly IBaseRepository<Poster> _baseRepository;
        private readonly IMapper _mapper;

        public PosterService(IBaseRepository<Poster> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(PosterDTO dto)
        {
            if (dto == null)
                return false;
            await _baseRepository.CreateAsync(_mapper.Map<Poster>(dto));
            return true;
        }

        public async Task<PosterDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<PosterDTO>(await _baseRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<PosterDTO>> GetAllAsync()
        {
            var items = _mapper.Map<IEnumerable<PosterDTO>>(await _baseRepository.GetAllAsync());
            if (!items.Any())
                return null;
            return items;
        }

        public async Task<bool> UpdateAsync(PosterDTO dto)
        {
            if (dto == null)
                return false;
            var item = await _baseRepository.GetByIdAsync(dto.Id);
            if (item == null)
                return false;
            item.DateTime = dto.DateTime;
            item.Premiere = dto.Premiere;
            item.PerformanceId = dto.PerformanceId;
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