using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specifications)  where TEntity : BaseEntity<TKey>
        {
            var Query = inputQuery; 

            if (specifications.Criteria != null)
            {
                Query = Query.Where(specifications.Criteria);
            }

            if (specifications.IncludeExpressions != null && specifications.IncludeExpressions.Count>0)
            {
                foreach (var includeExpression in specifications.IncludeExpressions)
                {
                    Query = Query.Include(includeExpression);
                }
            }

            return Query;
        }
    }
}
