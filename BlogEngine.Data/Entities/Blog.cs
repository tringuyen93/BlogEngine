using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Data.Entities
{
    public class Blog : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string RelativeLink { get; set; }
        public bool HasCommentsEnabled { get; set; }
        public bool IsChecked { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public bool CanUserDelete { get; set; }
        public bool CanUserEdit { get; set; }
        public string PageUrl { get; set; }

        public Category Category { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
