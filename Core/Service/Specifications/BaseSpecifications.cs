using DomainLayer.Contracts;
using DomainLayer.Models;
using System.Linq.Expressions;

namespace Service.Specifications
{
    abstract public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteriaExp)
        {
            Criteria = criteriaExp;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> includeExp) => IncludeExpressions.Add(includeExp);
    }
}
