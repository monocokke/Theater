using System;
using System.Collections.Generic;
using System.Text;
using Theater.Domain.Core.Models;
using Theater.Domain.Interfaces;

namespace Theater.Infrastructure.Data.Repositories
{
    public class ActorRoleRepository : IRepository<ActorRole>
    {
        private readonly TheaterContext db;

        public ActorRoleRepository(TheaterContext context)
        {
            db = context;
        }
        public void Create(ActorRole actorRole)
        {
            db.ActorRoles.Add(actorRole);
            db.SaveChanges();
        }
        public IEnumerable<ActorRole> GetList()
        {
            return db.ActorRoles;
        }

        public ActorRole Get(int id)
        {
            return db.ActorRoles.Find(id);
        }

        public void Delete(int id)
        {
            ActorRole actorRole = db.ActorRoles.Find(id);
            if (actorRole != null)
                db.ActorRoles.Remove(actorRole);
            db.SaveChanges();
        }
    }
}
