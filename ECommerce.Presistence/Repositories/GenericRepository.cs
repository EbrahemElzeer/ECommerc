using ECommerce.Domin.Contracts;
using ECommerce.Domin.Model;
using ECommerce.Presistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _storeDbContext;

        public GenericRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public async Task AddAsync(TEntity entity)
        {
          await _storeDbContext.Set<TEntity>().AddAsync(entity);
        }

        public void delete(TEntity entity)
        {
         _storeDbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
         return  await _storeDbContext.Set<TEntity>().ToListAsync();

        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,TKey> specifications)
        {
        IQueryable<TEntity> query = _storeDbContext.Set<TEntity>();
            if(specifications.IncludeExpression !=null && specifications.IncludeExpression.Any())
            {
                foreach(var includeExp in specifications.IncludeExpression)
                {
                                       query = query.Include(includeExp);
                }
            }
            return await query.ToListAsync();
        }
        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
          return  await _storeDbContext.Set<TEntity>().FindAsync(id);
        }
    }

}
