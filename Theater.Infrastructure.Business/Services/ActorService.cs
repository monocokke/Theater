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

        public bool Delete(int id)
        {
            actors.Delete(id);
            return true;
        }
    }
}
