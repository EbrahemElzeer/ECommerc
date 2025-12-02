using ECommerce.Domin.Contracts;
using ECommerce.Domin.Model;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }

            return query;
        }
    }
}
