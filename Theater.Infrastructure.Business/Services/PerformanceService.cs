using System.Collections.Generic;
using Theater.Domain.Core.Models;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;

namespace Theater.Infrastructure.Business.Services
{
    public class PerformanceService : IService<Performance>
    {
        private readonly IRepository<Performance> performances;

        public PerformanceService(IRepository<Performance> performanceRepository)
        {
            performances = performanceRepository;
        }
        public bool CreateItem(Performance performance)
        {
            performances.Create(performance);
            return true;
        }

        public Performance GetItem(int id)
        {
            return performances.Get(id);
        }

        public IEnumerable<Performance> GetItems()
        {
            return performances.GetList();
        }

        public bool Update(Performance performance)
        {
            var edited = performances.Get(performance.Id);
            if (edited == null)
                return false;
            if(performance.Name != null) { edited.Name = performance.Name; }
            if(performance.Genre != null) { edited.Genre = performance.Genre; }
            if(performance.Audience != null) { edited.Audience = performance.Audience; }
            performances.Update(performance);
            return true;
        }

        public bool Delete(int id)
        {
            performances.Delete(id);
            return true;
        }
    }
}