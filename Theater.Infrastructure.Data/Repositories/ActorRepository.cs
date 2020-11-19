using Theater.Domain.Interfaces;
using Theater.Domain.Core.Entities;
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

        public void Update(Actor actor)
        {
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            db.Actors.Remove(db.Actors.Find(id));
            db.SaveChanges();
        }
    }
}
