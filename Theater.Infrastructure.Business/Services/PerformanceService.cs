using System.Collections.Generic;
using Theater.Domain.Core.Entities;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;
using Theater.Domain.Core.DTO;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;

namespace Theater.Infrastructure.Business.Services
{
    public class PerformanceService : IBaseService<PerformanceDTO>
    {
        private readonly IBaseRepository<Performance> _baseRepository;
        private readonly IMapper _mapper;

        public PerformanceService(IBaseRepository<Performance> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(PerformanceDTO dto)
        {
            if (dto == null)
                return false;
            await _baseRepository.CreateAsync(_mapper.Map<Performance>(dto));
            return true;
        }

        public async Task<PerformanceDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<PerformanceDTO>(await _baseRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<PerformanceDTO>> GetAllAsync()
        {
            var items = _mapper.Map<IEnumerable<PerformanceDTO>>(await _baseRepository.GetAllAsync());
            if (!items.Any())
                return null;
            return items;
        }

        public async Task<bool> UpdateAsync(PerformanceDTO dto)
        {
            if (dto == null)
                return false;
            var item = await _baseRepository.GetByIdAsync(dto.Id);
            if (item == null)
                return false;
            item.Name = dto.Name;
            item.Genre = dto.Genre;
            item.Audience = dto.Audience;
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