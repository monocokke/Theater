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

        public bool Update(Poster poster)
        {
            var edited = posters.Get(poster.Id);
            if (edited == null)
                return false;
            if (poster.DateTime != null) { edited.DateTime = poster.DateTime; }
            if (poster.Premiere != null) { edited.Premiere = poster.Premiere; }
            posters.Update(poster);
            return true;
        }

        public bool Delete(int id)
        {
            posters.Delete(id);
            return true;
        }
    }
}
