using BookImporter.Entities.Models;
using BookImporter.Repositories.Interfaces;
using BookImporter.Repositories.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Repositories.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : Entity

    {
        public ApplicationDbContext _context { get; set; }

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public virtual async Task<TEntity?> GetAsync(int id)
        {
            return await _context.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public virtual void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
