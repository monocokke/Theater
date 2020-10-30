using System.Collections.Generic;
using Theater.Domain.Core.Models;
using Theater.Domain.Interfaces;
using Theater.Services.Interfaces;

namespace Theater.Infrastructure.Business.Services
{
    public class RoleService : IService<Role>
    {
        private readonly IRepository<Role> roles;

        public RoleService(IRepository<Role> roleRepository)
        {
            roles = roleRepository;
        }
        public bool CreateItem(Role role)
        {
            roles.Create(role);
            return true;
        }

        public Role GetItem(int id)
        {
            return roles.Get(id);
        }

        public IEnumerable<Role> GetItems()
        {
            return roles.GetList();
        }

        public bool Delete(int id)
        {
            roles.Delete(id);
            return true;
        }
    }
}
