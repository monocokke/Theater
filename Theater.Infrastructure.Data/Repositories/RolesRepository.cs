using Theater.Domain.Interfaces;
using Theater.Domain.Core.Models;
using System.Collections.Generic;

namespace Theater.Infrastructure.Data.Repositories
{
    public class RolesRepository : IRepository<Role>
    {
        private readonly TheaterContext db;

        public RolesRepository(TheaterContext context)
        {
            db = context;
        }
        public void Create(Role role)
        {
            db.TheaterRoles.Add(role);
            db.SaveChanges();
        }
        public IEnumerable<Role> GetList()
        {
            return db.TheaterRoles;
        }

        public Role Get(int id)
        {
            return db.TheaterRoles.Find(id);
        }

        public void Update(Role role)
        {
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            Role role = db.TheaterRoles.Find(id);
            if (role != null)
                db.TheaterRoles.Remove(role);
            db.SaveChanges();
        }
    }
}
