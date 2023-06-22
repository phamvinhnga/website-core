using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Website.Shared.Extensions
{
    public static class AuthExtension
    {
        public const string UserExtensionId = "UserExtensionId";

        public static int GetUserId(this IEnumerable<Claim> claims)
        {
            var value = GetClaimValueByType(claims, ClaimTypes.NameIdentifier);
            return value != null ? int.Parse(value) : 0;
        }

        public static string GetUserName(this IEnumerable<Claim> claims)
        {
            return GetClaimValueByType(claims, ClaimTypes.Name);
        }

        public static Guid? GetUserExtensionId(this IEnumerable<Claim> claims)
        {
            var value = GetClaimValueByType(claims, UserExtensionId);
            return value != null ? Guid.Parse(value) : null;
        }

        public static bool IsAdmin(this IEnumerable<Claim> claims)
        {
            var value = GetClaimValueByType(claims, ClaimTypes.Role);

            return value != null && value.ToUpper().Equals(RoleExtension.Admin.ToUpper());
        }

        public static bool IsStaff(this IEnumerable<Claim> claims)
        {
            var value = GetClaimValueByType(claims, ClaimTypes.Role);

            return value != null && value.ToUpper().Equals(RoleExtension.Staff.ToUpper());
        }

        private static string GetClaimValueByType(IEnumerable<Claim> claims, string claimType)
        {
            var enumerable = claims.ToList();
            if (!enumerable.Any() || string.IsNullOrEmpty(claimType))
            {
                return null;
            }
            var claim = enumerable.FirstOrDefault(c => c.Type.Equals(claimType));
            return claim?.Value;
        }
    }
}
