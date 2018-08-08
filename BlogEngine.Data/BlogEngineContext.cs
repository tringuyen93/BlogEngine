using System;
using Microsoft.EntityFrameworkCore;
using BlogEngine.Data.Entities;
using BlogEngine.Data.Interfaces;
using BlogEngine.Data.Mapping;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlogEngine.Data
{
    public class BlogEngineContext : DbContext, IDbContext
    {
        public BlogEngineContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<TEntity> Set<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            return base.Set<TEntity>();
        }

        public EntityEntry<TEntity> Entry<TEntity, TKey>(TEntity entity) where TEntity : BaseEntity<TKey>
        {
            return base.Entry(entity);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BlogMapping());
        }
    }

}
