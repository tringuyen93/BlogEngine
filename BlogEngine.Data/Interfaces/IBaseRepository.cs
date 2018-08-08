using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogEngine.Data.Interfaces
{
    public interface IBaseRepository<TEntity, TKey> : IDisposable
    {
        void SaveChange();
        IQueryable<TEntity> GetAll();
        TEntity GetByKey(TKey id);
        IQueryable<TEntity> Get<TOrderBy>(Expression<Func<TEntity, bool>> criteria,
            Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize, out int total,
            SortOrder sortOrder = SortOrder.Descending);
        IQueryable<TEntity> Get<TOrderBy>(Expression<Func<TEntity, TOrderBy>> orderBy, int pageIndex, int pageSize,
            out int total, SortOrder sortOrder = SortOrder.Descending);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> criteria);
        bool Update(TEntity entity);
        TEntity Add(TEntity entity);
        bool Delete(TKey id);

    }
}
