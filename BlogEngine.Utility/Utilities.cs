using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Primitives;

namespace BlogEngine.Utility
{
    public class Utilities
    {
        public static string GetUserId(ClaimsPrincipal user)
        {
            return user.FindFirst(OpenIdConnectConstants.Claims.Subject)?.Value?.Trim();
        }

        public static string[] GetRoles(ClaimsPrincipal identity)
        {
            return identity.Claims
                .Where(c => c.Type == OpenIdConnectConstants.Claims.Role)
                .Select(c => c.Value)
                .ToArray();
        }
    }
}
