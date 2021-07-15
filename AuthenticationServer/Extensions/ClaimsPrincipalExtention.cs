using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Extensions
{
    public static class ClaimsPrincipalExtention
    {
        public const string SERVICE_PERMISSIONS = "CwServicePermissions";
        public static string GetAccountName(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultNameClaimType);
            return claim?.Value;
        }

        //public static Roles GetAccountRole(this ClaimsPrincipal claimsPrincipal)
        //{
        //    var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimsIdentity.DefaultRoleClaimType);
        //    if (claim == null) return Roles.NoAuthorized;
        //    Enum.TryParse(claim.Value, out Roles role);
        //    return role;
        //}

        public static Guid GetAccountId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid);
            if (claim == null) return Guid.Empty;
            return new Guid(claim.Value);
        }

        public static string GetAccountLastIp(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
            return claim?.Value;
        }

        public static bool CheckAccountServicePermission(this ClaimsPrincipal claimsPrincipal, string serviceKey)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == SERVICE_PERMISSIONS);
            if (claim == null) return false;
            var permissions = JsonSerializer.Deserialize<List<KeyValuePair<string, bool>>>(claim.Value);
            return permissions.FirstOrDefault(c => c.Key == serviceKey).Value;
        }
    }
}
