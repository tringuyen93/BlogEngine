using Microsoft.EntityFrameworkCore;
using BlogEngine.Data.Entities;
using BlogEngine.Data.Interfaces;
using BlogEngine.Data.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BlogEngine.Data
{
    public class BlogEngineContext : IdentityDbContext<User, Role, string>, IDbContext
    {
        public string CurrentUserId { get; set; }
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
            builder.ApplyConfiguration(new UserMapping());
            builder.ApplyConfiguration(new RoleMapping());
            builder.ApplyConfiguration(new RoleClaimsMapping());
            builder.ApplyConfiguration(new UserClaimMapping());
            builder.ApplyConfiguration(new UserLoginMapping());
            builder.ApplyConfiguration(new UserRoleMapping());
            builder.ApplyConfiguration(new UserTokenMapping());
            builder.ApplyConfiguration(new TagMapping());
            builder.ApplyConfiguration(new CategoryMapping());
            builder.ApplyConfiguration(new CommentMapping());
        }
    }

}
