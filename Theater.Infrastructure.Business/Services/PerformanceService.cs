using System.Collections.Generic;
using Theater.Domain.Core.Entities;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;
using Theater.Domain.Core.DTO;
using AutoMapper;

namespace Theater.Infrastructure.Business.Services
{
    public class PerformanceService : IService<PerformanceDTO>
    {
        private readonly IRepository<Performance> _performances;
        private readonly IMapper _mapper;

        public PerformanceService(IRepository<Performance> performanceRepository, IMapper mapper)
        {
            _performances = performanceRepository;
            _mapper = mapper;
        }
        public bool CreateItem(PerformanceDTO performanceDTO)
        {            
            _performances.Create(_mapper.Map<Performance>(performanceDTO));
            return true;
        }

        public PerformanceDTO GetItem(int id)
        {
            return _mapper.Map<PerformanceDTO>(_performances.Get(id));
        }

        public IEnumerable<PerformanceDTO> GetItems()
        {
            return _mapper.Map<IEnumerable<PerformanceDTO>>(_performances.GetList());
        }

        public bool Update(PerformanceDTO performanceDTO)
        {
            var edited = _performances.Get(performanceDTO.Id);
            if (edited == null)
                return false;
            if(performanceDTO.Name != null) { edited.Name = performanceDTO.Name; }
            if(performanceDTO.Genre != null) { edited.Genre = performanceDTO.Genre; }
            if(performanceDTO.Audience != null) { edited.Audience = performanceDTO.Audience; }
            _performances.Update(_mapper.Map<Performance>(performanceDTO));
            return true;
        }

        public bool Delete(int id)
        {
            if (id > 0 && _performances.Get(id) != null)
            {
                _performances.Delete(id);
            }
            return true;
        }
    }
}