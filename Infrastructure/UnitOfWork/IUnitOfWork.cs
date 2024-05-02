using Infrastructure.Reposatory;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> SaveChangesWithoutDisposeAsync();
    }
}
