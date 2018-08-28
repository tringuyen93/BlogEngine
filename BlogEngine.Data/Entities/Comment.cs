using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Data.Entities
{
    public class Comment : BaseEntity<Guid>
    {
        public bool IsDeleted { get; set; }
        public bool IsPending { get; set; }
        public bool IsApproved { get; set; }
        public bool IsSpam { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public bool HasChildren { get; set; }
        public string Title { get; set; }
        public string DateCreated { get; set; }
        public string RelativeLink { get; set; }
        public Guid BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
