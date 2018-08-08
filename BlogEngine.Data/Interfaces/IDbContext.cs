using System;
using BlogEngine.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlogEngine.Data.Interfaces
{
    public interface IDbContext : IDisposable
    {
        int SaveChanges();
        DbSet<TEntity> Set<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        EntityEntry<TEntity> Entry<TEntity, TKey>(TEntity entity) where TEntity : BaseEntity<TKey>;
    }
}
