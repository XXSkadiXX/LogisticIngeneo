using Infraestructure.Core.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Infraestructure.Core.Repository
{
    [ExcludeFromCodeCoverage]
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbcontext;
        private readonly DbSet<TEntity> _entities;

        public Repository(DbContext dbcontext)
        {
            this._dbcontext = dbcontext;
            this._entities = dbcontext.Set<TEntity>();
        }

        private IQueryable<TEntity> PerformInclusions(IEnumerable<Expression<Func<TEntity, object>>> includeProperties,
                                               IQueryable<TEntity> query)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


        #region IRepository<TEntity> Members

        public IQueryable<TEntity> AsQueryable()
        {
            return _entities.AsQueryable<TEntity>();
        }

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = AsQueryable();
            return PerformInclusions(includeProperties, query);
        }


        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = AsQueryable().AsNoTracking();
            query = PerformInclusions(includeProperties, query);
            return query.Where(where);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return query.AsNoTracking().FirstOrDefault(where);
        }

        public void Insert(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _entities.Attach(entity);
            _dbcontext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (_dbcontext.Entry(entity).State == EntityState.Detached)
            {
                _entities.Attach(entity);
            }
            _entities.Remove(entity);
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = _entities.Find(id);
            _entities.Remove(entityToDelete);
        }

        #endregion IRepository<TEntity> Members

    }

}
