using BookImporter.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookImporter.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : Entity
    {
        public Task<TEntity?> GetAsync(int id);

        public Task<IEnumerable<TEntity>> GetAllAsync();

        public Task<TEntity> AddAsync(TEntity entity);

        public Task<TEntity> UpdateAsync(TEntity entity);

        public void Remove(TEntity entity);
    }
}
