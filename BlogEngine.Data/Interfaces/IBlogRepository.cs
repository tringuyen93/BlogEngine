using System;
using System.Collections.Generic;
using System.Text;
using BlogEngine.Data.Entities;

namespace BlogEngine.Data.Interfaces
{
    public interface IBlogRepository : IBaseRepository<Blog, Guid>
    {
    }
}
