using ECommerce.Domin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domin.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity: BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity,TKey> specifications);
        Task AddAsync(TEntity entity);
        Task<int> CountAsync(ISpecifications<TEntity,TKey> specifications);
        void delete(TEntity entity);
    }
}
