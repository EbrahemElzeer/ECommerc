using ECommerce.Domin.Contracts;
using ECommerce.Domin.Model;
using ECommerce.Presistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _storeDbContext;
        private readonly Dictionary<Type, object> _repositories = [];
        public UnitOfWork(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public  IGenericRepository<TEntity, TKey> GetRepostory<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
          var entityType= typeof(TEntity);
            if (_repositories.TryGetValue(entityType,out var repository))
            {
                return  (IGenericRepository <TEntity, TKey>)repository;

            }
            var newRepository = new GenericRepository<TEntity, TKey>(_storeDbContext);
            _repositories[entityType] = newRepository;
            return newRepository;
        }

        public async Task<int> SaveChangesAsync() =>await _storeDbContext.SaveChangesAsync();
        
           
        
    }
}
