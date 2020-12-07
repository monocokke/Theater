using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Theater.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
