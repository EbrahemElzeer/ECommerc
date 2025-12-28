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
        protected BaseSpecifications(Expression<Func<TEntity,bool>> expression )
        {
             Criteria=expression;
        }
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];

       public Expression<Func<TEntity, bool>> Criteria { get; }

        public Expression<Func<TEntity, object>> OrderBy {private set; get; }

        public Expression<Func<TEntity, object>> OrderByDescending { private set; get; }

        public int Take { get; set; }
        public int Skip { get; set; }

        public bool IsPaginated { get; private set; } 


        protected void ApplyPagination(int pageSize , int PageIndex)
        {
         
            IsPaginated = true;
            Take = pageSize;
            Skip=(PageIndex - 1) * pageSize;
        }

        protected void AddInclude (Expression<Func<TEntity,object>> IncludeExp)
        {
            IncludeExpression.Add(IncludeExp);
        }



        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp)
        {
            OrderBy = orderByExp;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDesExp)
        {
            OrderByDescending = orderByDesExp;
        }


    }
}


