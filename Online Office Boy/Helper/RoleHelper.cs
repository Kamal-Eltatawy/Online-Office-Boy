using Domain.Const;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;

namespace Online_Office_Boy.Helper
{
    public static class RoleHelper
    {
        public static async Task AddPermissionClaims (this RoleManager<IdentityRole> roleManager, IdentityRole role, Modules model,bool isView =false)
        {
            if(role != null)
            {
               var roleClaims= await roleManager.GetClaimsAsync (role);
                List<string> permissions = new List<string> (); 
                if(isView)
                {
                 permissions  = Permission.GenerateViewPermissions(model);
                }
                permissions = Permission.GenerateEditUpdateDeletePermissions(model);
                foreach (var permission in permissions)
                {
                    if (!roleClaims.Any(c => c.Type == ClaimsTypes.Permissions.ToString() && c.Value == permission))
                    {
                        var claim = new Claim(ClaimsTypes.Permissions.ToString(), permission);
                        await roleManager.AddClaimAsync(role, claim);
                        await roleManager.UpdateAsync(role);
                    }
                }
            }
        }

        public static async Task AddAllPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, bool isView = false)
        {
            if (role != null)
            {
                var roleClaims = await roleManager.GetClaimsAsync(role);
                List<string> permissions = new List<string>();
                if (isView)
                {
                    permissions = Permission.GenerateAllPermissions(isView);
                }
                permissions = Permission.GenerateAllPermissions();
                foreach (var permission in permissions)
                {
                    if (!roleClaims.Any(c => c.Type == ClaimsTypes.Permissions.ToString() && c.Value == permission))
                    {
                        var claim = new Claim(ClaimsTypes.Permissions.ToString(), permission);
                        await roleManager.AddClaimAsync(role, claim);
                        await roleManager.UpdateAsync(role);
                    }
                }
            }
        }

    }
}
