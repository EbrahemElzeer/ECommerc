using ECommerce.Domin.Contracts;
using ECommerce.Domin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];

        protected void AddInclude (Expression<Func<TEntity,object>> IncludeExp)
        {
            IncludeExpression.Add(IncludeExp);
        }






    }
}


