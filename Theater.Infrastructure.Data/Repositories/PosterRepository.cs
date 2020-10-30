using Theater.Domain.Interfaces;
using Theater.Domain.Core.Models;
using System.Collections.Generic;

namespace Theater.Infrastructure.Data.Repositories
{
    public class PosterRepository : IRepository<Poster>
    {
        private readonly TheaterContext db;

        public PosterRepository(TheaterContext context)
        {
            db = context;
        }
        public void Create(Poster poster)
        {
            db.Posters.Add(poster);
            db.SaveChanges();
        }
        public IEnumerable<Poster> GetList()
        {
            return db.Posters;
        }

        public Poster Get(int id)
        {
            return db.Posters.Find(id);
        }

        public void Delete(int id)
        {
            Poster poster = db.Posters.Find(id);
            if (poster != null)
                db.Posters.Remove(poster);
            db.SaveChanges();
        }
    }
}