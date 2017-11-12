using GoodiesMarket.Components.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace GoodiesMarket.API.Extensions
{
    public static class IdentityExtensions
    {
        public static RoleType GetRole(this IIdentity identity)
        {
            var roleName = GetRoleName(identity);

            return (RoleType)Enum.Parse(typeof(RoleType), roleName);
        }

        public static string GetRoleName(this IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity)identity;
            return claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
        }

        public static IEnumerable<RoleType> GetRoles(this IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity)identity;
            return claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Role)
                .Select(c => (RoleType)Enum.Parse(typeof(RoleType), c.Value, true));
        }

        public static IEnumerable<string> GetRolesName(this IIdentity identity)
        {
            var claimsIdentity = (ClaimsIdentity)identity;
            return claimsIdentity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
        }

        public static Guid GetId(this IIdentity identity)
        {
            return Guid.Parse(identity.GetUserId());
        }
    }
}