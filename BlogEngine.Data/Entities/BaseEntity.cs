using System;

namespace BlogEngine.Data.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        string CreatedBy { get; set; }
        string UpdatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
