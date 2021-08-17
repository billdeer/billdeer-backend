using Billdeer.Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Core.DataAccess.EntityFreamwork
{
    public class EfEntityRepositoryBase<TEntity, TContext>
        : IEntityRepository<TEntity>
        where TEntity: class,IEntity, new()
        where TContext: DbContext
    {
        protected readonly TContext _context;

        public EfEntityRepositoryBase(TContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            return _context.Add(entity).Entity;
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.AddRange(entities);
        }

        public TEntity Update(TEntity entity)
        {
            return _context.Update(entity).Entity;
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _context.RemoveRange(entities);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().FirstOrDefault(expression);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>().AsQueryable().FirstOrDefaultAsync(expression);
        }

        public int GetCount(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null 
                ? _context.Set<TEntity>().Count()
                : _context.Set<TEntity>().Count(expression);
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? await _context.Set<TEntity>().CountAsync()
                : await _context.Set<TEntity>().CountAsync(expression);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? _context.Set<TEntity>().AsNoTracking()
                : _context.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().Where(expression).ToListAsync();
        }

        public IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> expression = null )
        {
            return expression == null
                ? _context.Set<TEntity>()
                : _context.Set<TEntity>().Where(expression);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
