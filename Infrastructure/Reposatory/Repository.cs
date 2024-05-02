using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Reposatory
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<TEntity> GetByAsync(Expression<Func<TEntity,bool>> expression)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsQueryable().ToListAsync();
        }
        public async Task<List<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AsNoTracking().AnyAsync(predicate);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public  void Update(TEntity entity)
        {
             _context.Set<TEntity>().Update(entity);
        }
        public void UpdateWithoutTracking(TEntity entity) 
        {
                _context.Set<TEntity>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<List<TEntity>> GetAllIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>> []includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable().AsNoTracking().Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();

        }

        public async Task<List<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }



        public async Task DeleteAsync(int id)
        {
            {
                try
                {
                    var entity = await GetByIdAsync(id);
                    if (entity != null)
                    {
                        _context.Set<TEntity>().Remove(entity);
                    }
                }
                catch (Exception ex)
                {
                    throw new RepositoryException("Error occurred while deleting entity", ex);
                }
            }
        }

        public void Delete(TEntity entity)
        {
            {
                try
                {
                    if (entity != null)
                    {
                        _context.Set<TEntity>().Remove(entity);
                    }
                }
                catch (Exception ex)
                {
                    throw new RepositoryException("Error occurred while deleting entity", ex);
                }
            }
        }

        public async Task<TEntity> GetIncludingAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query?.FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable().AsNoTracking().Where(predicate);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query?.FirstOrDefaultAsync();
        }
    }
}
