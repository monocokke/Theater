using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theater.Domain.Core.DTO;
using Theater.Domain.Core.Entities;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;

namespace Theater.Infrastructure.Business.Services
{
    public class ActorRoleService : IBaseService<ActorRoleDTO>
    {
        private readonly IBaseRepository<ActorRole> _baseRepository;       
        private readonly IMapper _mapper;

        public ActorRoleService(IBaseRepository<ActorRole> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(ActorRoleDTO dto)
        {
            if (dto == null)
                return false;
            await _baseRepository.CreateAsync(_mapper.Map<ActorRole>(dto));
            return true;
        }

        public async Task<ActorRoleDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<ActorRoleDTO>(await _baseRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<ActorRoleDTO>> GetAllAsync()
        {
            var items = _mapper.Map<IEnumerable<ActorRoleDTO>>(await _baseRepository.GetAllAsync());
            if (!items.Any())
                return null;
            return items;
        }

        public async Task<bool> UpdateAsync(ActorRoleDTO dto)
        {
            if (dto == null)
                return false;
            var item = await _baseRepository.GetByIdAsync(dto.Id);
            if (item == null)
                return false;
            item.ActorId = dto.ActorId;
            item.RoleId = dto.RoleId;
            item.Understudy = dto.Understudy;
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
