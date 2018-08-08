using System;
using System.Collections.Generic;
using System.Text;
using BlogEngine.Data.Entities;
using BlogEngine.Data.Interfaces;

namespace BlogEngine.Data.Repositories
{
    public class BlogRepository : BaseRepository<Blog, Guid>, IBlogRepository
    {
        public BlogRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
