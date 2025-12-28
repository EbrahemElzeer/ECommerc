using ECommerce.Domin.Contracts;
using ECommerce.Domin.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Presistence
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity>entryPoint,ISpecifications<TEntity,TKey> specifications)where TEntity :BaseEntity<TKey>
        {
            var query = entryPoint;

            if(specifications is not null)
            {
                if(specifications.IncludeExpression != null && specifications.IncludeExpression.Any())
                {
                    query= specifications.IncludeExpression.Aggregate(query, (current, includeExp) => current.Include(includeExp));
                }
                if (specifications.Criteria != null)
                {
                                       query= query.Where(specifications.Criteria);
                }

                if(specifications.OrderBy is not null)
                {
                    query=query.OrderBy(specifications.OrderBy);
                }
                if(specifications.OrderByDescending is not null)
                {
                    query=query.OrderByDescending(specifications.OrderByDescending);
                }

                if(specifications.IsPaginated)
                {
                    query=query.Skip(specifications.Skip).Take(specifications.Take);
                }


            }

            return query;
        }
    }
}
