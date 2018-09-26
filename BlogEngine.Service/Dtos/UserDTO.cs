using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Service.Dtos
{
    public class UserDTO : BaseDTO<string>
    {
        public string Birthday { get; set; }
        public string PhotoUrl { get; set; }
        public string JobTitle { get; set; }
        public string FullName { get; set; }
        public string Configuration { get; set; }
        public bool IsEnabled { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<IdentityUserRole<string>> RoleIds { get; set; }
        public string[] Rolses { get; set; }
    }
}
