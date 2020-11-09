using System;
using System.Collections.Generic;
using System.Text;
using Theater.Domain.Core.Models;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;

namespace Theater.Infrastructure.Business.Services
{
    public class ActorRoleService : IService<ActorRole>
    {
        private readonly IRepository<ActorRole> actorRoles;

        public ActorRoleService(IRepository<ActorRole> actorRoleRepository)
        {
            actorRoles = actorRoleRepository;
        }
        public bool CreateItem(ActorRole actorRole)
        {
            actorRoles.Create(actorRole);
            return true;
        }

        public ActorRole GetItem(int id)
        {
            return actorRoles.Get(id);
        }

        public IEnumerable<ActorRole> GetItems()
        {
            return actorRoles.GetList();
        }

        public bool Update(ActorRole actorRole)
        {
            var edited = actorRoles.Get(actorRole.Id);
            if (edited == null)
                return false;
            if (actorRole.ActorId != null) { edited.ActorId = actorRole.ActorId; }
            if (actorRole.RoleId != null) { edited.RoleId = actorRole.RoleId; }
            if (actorRole.Understudy != null) { edited.Understudy = actorRole.Understudy; }
            actorRoles.Update(actorRole);
            return true;
        }

        public bool Delete(int id)
        {
            actorRoles.Delete(id);
            return true;
        }
    }
}
