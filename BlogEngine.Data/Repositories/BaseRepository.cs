using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using BlogEngine.Data.Entities;
using BlogEngine.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Data.Repositories
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>

    {
        private readonly IDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
            this._dbSet = dbContext.Set<TEntity, TKey>();
        }

        public void Dispose()
        {
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
            }

        }

        public void SaveChange()
        {
            _dbContext.SaveChanges();
        }
        
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public TEntity GetByKey(TKey id)
        {
            return _dbSet.AsQueryable().SingleOrDefault(x => x.Id.Equals(id));
        }

        public IQueryable<TEntity> Get<TOrderBy>(Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, out int total,
            SortOrder sortOrder = SortOrder.Descending)
        {
            total = _dbSet.Count();
            return sortOrder == SortOrder.Ascending
                ? _dbSet
                    .AsQueryable()
                    .Where(criteria)
                    .OrderBy(orderBy)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                : _dbSet
                    .AsQueryable()
                    .Where(criteria)
                    .OrderByDescending(orderBy)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);

        }

        public IQueryable<TEntity> Get<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, out int total,
            SortOrder sortOrder = SortOrder.Descending)
        {
            total = _dbSet.Count();
            return sortOrder == SortOrder.Ascending
                ? _dbSet
                    .AsQueryable()
                    .OrderBy(orderBy)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                : _dbSet
                    .AsQueryable()
                    .OrderByDescending(orderBy)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);

        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> criteria)
        {
            return _dbSet.Where(criteria).AsQueryable();
        }

        public bool Update(TEntity entity)
        {
            var original = GetByKey(entity.Id);
            if (original != null)
            {
                _dbContext.Entry<TEntity, TKey>(original).CurrentValues.SetValues(entity);
                return true;
            }
            return false;
        }

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        public bool Delete(TKey id)
        {
            var entity = GetByKey(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }
            return false;
        }
    }
}
