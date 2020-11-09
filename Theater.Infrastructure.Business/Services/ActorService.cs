using System.Collections.Generic;
using Theater.Domain.Core.Models;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;

namespace Theater.Infrastructure.Business.Services
{
    public class ActorService : IService<Actor>
    {
        private readonly IRepository<Actor> actors;

        public ActorService(IRepository<Actor> actorRepository)
        {
            actors = actorRepository;
        }
        public bool CreateItem(Actor actor)
        {
            actors.Create(actor);
            return true;
        }

        public Actor GetItem(int id)
        {
            return actors.Get(id);
        }

        public IEnumerable<Actor> GetItems()
        {
            return actors.GetList();
        }

        public bool Update(Actor actor)
        {
            var edited = actors.Get(actor.Id);
            if (edited == null)
                return false;
            if (actor.EyeColor != null) { edited.EyeColor = actor.EyeColor; }
            if (actor.HairColor != null) { edited.HairColor = actor.HairColor; }
            if (actor.Nationality != null) { edited.Nationality = actor.Nationality; }
            if (actor.Height != null) { edited.Height = actor.Height; }
            actors.Update(actor);
            return true;
        }

        public bool Delete(int id)
        {
            actors.Delete(id);
            return true;
        }
    }
}
