using Billdeer.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Billdeer.Core.DataAccess
{
    public interface IEntityRepository<TEntity> where TEntity: class, IEntity, new()
    {
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        TEntity Get(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);

        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> expression = null);
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression = null);

        IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> expression);
        Task<IQueryable<TEntity>> QueryableAsync(Expression<Func<TEntity, bool>> expression);

        int SaveChanges(); // UnitOfWork Patterni ayrı klasow olmadan repositoryde
        Task<int> SaveChangesAsync();

        int GetCount(Expression<Func<TEntity, bool>> expression = null);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> expression = null);


    }
}
