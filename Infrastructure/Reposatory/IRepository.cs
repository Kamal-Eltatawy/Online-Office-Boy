using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Reposatory
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetIncludingAsync(params Expression<Func<TEntity, object>>[] includes);

        Task<List<TEntity>> GetAllIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        void UpdateWithoutTracking(TEntity entity);
        Task<TEntity> GetByIdAsync(int id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task DeleteAsync(int id);
        void Delete(TEntity entity);
    }
}
