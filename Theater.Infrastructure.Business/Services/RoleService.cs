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

        public bool Update(Role role)
        {
            var edited = roles.Get(role.Id);
            if (edited == null)
                return false;
            if (role.Name != null) { edited.Name = role.Name; }
            if (role.Age != null) { edited.Age = role.Age; }
            if (role.Sex != null) { edited.Sex = role.Sex; }
            if (role.EyeColor != null) { edited.EyeColor = role.EyeColor; }
            if (role.HairColor != null) { edited.HairColor = role.HairColor; }
            if (role.Nationality != null) { edited.Nationality = role.Nationality; }
            if (role.Height != null) { edited.Height = role.Height; }
            if (role.PerformanceId != null) { edited.PerformanceId = role.PerformanceId; }
            roles.Update(role);
            return true;
        }

        public bool Delete(int id)
        {
            roles.Delete(id);
            return true;
        }
    }
}
