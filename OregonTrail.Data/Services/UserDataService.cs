using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OregonTrail.Data.Context;
using OregonTrail.Models.Shared;
using OregonTrail.Models.Shared.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OregonTrail.Data.Services
{
    public class UserDataService
    {
        private readonly OregonTrailDBContext context;

        public UserDataService(OregonTrailDBContext dbContext)
        {
            context = dbContext;
        }

        public IQueryable<UserRoleView> GetQueryableUserRoles()
        {
            var userRoles = (from user in context.Users
                             from userRole in context.UserRoles
                                .Where(userRole => user.Id == userRole.UserId)
                                .DefaultIfEmpty() // ensures this join is a left join
                             from role in context.Roles
                                .Where(role => role.Id == userRole.RoleId)
                                .DefaultIfEmpty()  // ensures this join is a left join
                             select new UserRoleView()
                             {
                                 UserName = user.UserName,
                                 Email = user.Email,
                                 Role = !string.IsNullOrEmpty(role.Name) ? Enum.Parse<Role>(role.Name) : (Role?)null // Need to explictily cast a nullable (https://stackoverflow.com/questions/828950/why-doesnt-this-c-sharp-code-compile)
                             }).AsQueryable();

            return userRoles;
        }
    }
}
