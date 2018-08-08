using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Data.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
