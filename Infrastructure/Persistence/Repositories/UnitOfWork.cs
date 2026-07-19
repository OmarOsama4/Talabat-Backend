using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (repositories.ContainsKey(typeName)) return (IGenericRepository<TEntity, TKey>)repositories[typeName];

            else
            {
                var repo = new GenericRepository<TEntity, TKey>(_dbContext);
                repositories["typeName"] = repo;
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
