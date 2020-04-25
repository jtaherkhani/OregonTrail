using Microsoft.AspNetCore.Components;
using OregonTrail.Models.Shared;
using OregonTrail.Models.Shared.Enums;
using OregonTrail.UI.Client.Helpers;
using OregonTrail.UI.Client.Services;
using OregonTrail.UI.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OregonTrail.UI.Client.Pages
{
    public class UsersCode : ComponentBase
    {
        [Inject]
        public UserService UserService { get; set; }

        public List<UserRoleView> users; // Complete set of users that can be displayed on the grid.
        public List<UserRoleView> filteredUsers; // User set displayed in the grid that can be filtered by enum value.

        public int totalUsers;
        public Role CurrentRole;

        protected override async Task OnInitializedAsync() // todo: try after render to see if it's more performant?
        {
            var paginationResponse = await UserService.GetPaginatedUserRoles(new PaginationRequstDTO());

            users = paginationResponse.Content;
            filteredUsers = users;

            totalUsers = paginationResponse.TotalRecordCount;
        }

        public List<object> GetRoleDropDown()
        {
            return EnumHelper.CreateEnumDropDown<Role>();
        }

        public void ChangeRoleDropDown(object newRoleValue)
        {
            var role = (Role)newRoleValue;
            filteredUsers = users.Where(x => x.Role == role).ToList();
        }
    }
}
