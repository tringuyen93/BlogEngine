using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Data.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
