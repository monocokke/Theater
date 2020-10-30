using Theater.Domain.Interfaces;
using Theater.Domain.Core.Models;
using System.Collections.Generic;

namespace Theater.Infrastructure.Data.Repositories
{
    public class ActorRepository : IRepository<Actor>
    {
        private readonly TheaterContext db;

        public ActorRepository(TheaterContext context)
        {
            db = context;
        }
        public void Create(Actor actor)
        {
            db.Actors.Add(actor);
            db.SaveChanges();
        }
        public IEnumerable<Actor> GetList()
        {
            return db.Actors;
        }

        public Actor Get(int id)
        {
            return db.Actors.Find(id);
        }

        public void Delete(int id)
        {
            Actor actor = db.Actors.Find(id);
            if (actor != null)
                db.Actors.Remove(actor);
            db.SaveChanges();
        }
    }
}
