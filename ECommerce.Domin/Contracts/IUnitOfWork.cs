using ECommerce.Domin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domin.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenericRepository<TEntity,TKey> GetRepostory<TEntity, TKey>() where TEntity:BaseEntity<TKey>;

    }
}
