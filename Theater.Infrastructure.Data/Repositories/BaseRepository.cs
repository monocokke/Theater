using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Theater.Domain.Interfaces;

namespace Theater.Infrastructure.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _entities;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id) => await _entities.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _entities.AsNoTracking().ToListAsync();

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
