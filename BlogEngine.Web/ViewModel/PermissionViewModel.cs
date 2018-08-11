using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.WebApi.ViewModel
{
    public class PermissionViewModel
    {
        public string RoleId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
    }
}
