using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Service.Dtos
{
    public class BlogDTO : BaseDTO<Guid>
    {
        public bool IsChecked { get; set; }
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsActive { get; set; }
        public bool CanUserDelete { get; set; }
        public bool CanUserEdit { get; set; }
    }
}
