using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Service.Dtos
{
    public class BaseDTO<TKey>
    {
        public TKey Id { get; set; }
    }
}
