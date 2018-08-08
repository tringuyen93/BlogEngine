using System;
using System.Collections.Generic;
using System.Text;
using BlogEngine.Data.Entities;
using BlogEngine.Data.Interfaces;

namespace BlogEngine.Data.Interface
{
    public interface IUnitOfWork<TKey> : IDisposable
    {
        IBaseRepository<TEntity, TKey> GetRepository<TEntity>() where TEntity : BaseEntity<TKey>;
        void CommitChange();
    }
}
