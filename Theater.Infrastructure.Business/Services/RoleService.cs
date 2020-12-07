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
    public class RoleService : IBaseService<RoleDTO>
    {
        private readonly IBaseRepository<Role> _baseRepository;
        private readonly IMapper _mapper;

        public RoleService(IBaseRepository<Role> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(RoleDTO dto)
        {
            if (dto == null)
                return false;
            await _baseRepository.CreateAsync(_mapper.Map<Role>(dto));
            return true;
        }

        public async Task<RoleDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<RoleDTO>(await _baseRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            var items = _mapper.Map<IEnumerable<RoleDTO>>(await _baseRepository.GetAllAsync());
            if (!items.Any())
                return null;
            return items;
        }

        public async Task<bool> UpdateAsync(RoleDTO dto)
        {
            if (dto == null)
                return false;
            var item = await _baseRepository.GetByIdAsync(dto.Id);
            if (item == null)
                return false;
            item.Name = dto.Name;
            item.Age = dto.Age;
            item.Sex = dto.Sex;
            item.EyeColor = dto.EyeColor;
            item.HairColor = dto.HairColor;
            item.Nationality = dto.Nationality;
            item.Height = dto.Height;
            item.Description = dto.Description;
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