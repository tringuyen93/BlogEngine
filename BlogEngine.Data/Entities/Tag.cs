using System;

namespace BlogEngine.Data.Entities
{
    public class Tag : BaseEntity<Guid>
    {
        public string TagName { get; set; }
        public Blog Blog { get; set; }
    }
}