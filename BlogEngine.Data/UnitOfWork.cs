using System;
using System.Collections.Generic;
using System.Text;
using BlogEngine.Data.Entities;
using BlogEngine.Data.Interface;
using BlogEngine.Data.Interfaces;
using BlogEngine.Data.Repositories;

namespace BlogEngine.Data
{
    public class UnitOfWork<TKey> : IUnitOfWork<TKey>
    {
        private IDbContext _dbContext;
        private Dictionary<Type, object> repositories;
        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBaseRepository<TEntity, TKey> GetRepository<TEntity>() where TEntity : BaseEntity<TKey>
        {
            if (this.repositories == null)
            {
                this.repositories = new Dictionary<Type, object>();
            }
            var type = typeof(TEntity);
            if (this.repositories.ContainsKey(type))
            {
                this.repositories[type] = new BaseRepository<TEntity, TKey>(this._dbContext);
            }
            return (IBaseRepository<TEntity, TKey>)repositories[type];
        }

        public void CommitChange()
        {
            this._dbContext.SaveChanges();
        }
        public void Dispose()
        {
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
                _dbContext = null;
            }
        }
    }
}
