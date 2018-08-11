using BlogEngine.Utility;
using Microsoft.AspNetCore.Identity;

namespace BlogEngine.WebApi.Helper
{
    public class ConvertRoleClaim
    {
        public static Permission Convert(IdentityRoleClaim<string> s)
        {
            var tmp = Permissions.GetPermissionByValue(s.ClaimValue);
            return tmp;
        }
    }
}
