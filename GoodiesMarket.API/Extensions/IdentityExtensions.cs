using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace GoodiesMarket.API.Extensions
{
    public static class IdentityExtensions
    {
        public static IEnumerable<string> GetRoles(this IIdentity identity)
        {
            return ((ClaimsIdentity)identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
        }
    }
}