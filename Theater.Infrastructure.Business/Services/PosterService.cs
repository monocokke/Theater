using System.Collections.Generic;
using Theater.Domain.Core.Models;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;

namespace Theater.Infrastructure.Business.Services
{
    public class PosterService : IService<Poster>
    {
        private readonly IRepository<Poster> posters;

        public PosterService(IRepository<Poster> posterRepository)
        {
            posters = posterRepository;
        }
        public bool CreateItem(Poster poster)
        {
            posters.Create(poster);
            return true;
        }

        public Poster GetItem(int id)
        {
            return posters.Get(id);
        }

        public IEnumerable<Poster> GetItems()
        {
            return posters.GetList();
        }

        public bool Delete(int id)
        {
            posters.Delete(id);
            return true;
        }
    }
}
