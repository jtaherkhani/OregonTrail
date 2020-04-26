using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OregonTrail.Models.Shared;
using OregonTrail.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OregonTrail.Data.Context
{
    /// <summary>
    /// Seed data that requires context to ensure it is seed as expected.
    /// This will run at startup of the application as opposed to during migration, this will ensure that we don't take admin usernames into the migration file.
    /// </summary>
    public static class OregonTrailDBInitializer
    {
        public static void SeedIdentity(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, AdminOptions options)
        {
            SeedRoles(roleManager);
            if (options != null) // If no configuration exists for admin then our Admin options will not bind.
            {
                SeedAdminUser(userManager, options);
            }
        }
        
        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleName in Enum.GetNames(typeof(Role)))
            {
                if (roleManager.FindByNameAsync(roleName).Result == null)
                {
                    roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = roleName
                    }).GetAwaiter().GetResult(); // force synchronous on creation
                }
            }
        }

        private static void SeedAdminUser(UserManager<IdentityUser> userManager, AdminOptions options)
        {
            var foundUser = userManager.FindByNameAsync(options.UserName).Result;
                
            if (foundUser == null)
            {
                var newUser = new IdentityUser()
                {
                    UserName = options.UserName,
                    Email = options.Email
                }
;
                var result = userManager.CreateAsync(newUser, options.Password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(newUser, Enum.GetName(typeof(Role), Role.Admin)).GetAwaiter().GetResult();
                }
            }
        }
    }
}
