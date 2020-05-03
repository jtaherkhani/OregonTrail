using Microsoft.AspNetCore.Components;
using OregonTrail.Models.Shared;
using OregonTrail.Models.Shared.Enums;
using OregonTrail.UI.Client.Helpers;
using OregonTrail.UI.Client.Pages.Modals;
using OregonTrail.UI.Client.Services;
using OregonTrail.UI.Shared.DTOs;
using Radzen;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OregonTrail.UI.Client.Pages
{
    public class UsersCode : ComponentBase
    {
        [Inject]
        public UserService UserService { get; set; }

        [Inject]
        public UserInviteService UserInviteService { get; set; }

        [Inject]
        public DialogService DialogService { get; set; }

        public List<UserRoleView> users; // Complete set of users that can be displayed on the grid.
        public List<UserRoleView> filteredUsers; // User set displayed in the grid that can be filtered by enum value.

        public int totalUsers;
        public Role CurrentRole;

        protected override async Task OnInitializedAsync() // todo: try after render to see if it's more performant?
        {
            DialogService.OnClose += CloseDialog; // need to map the closing of the dialog service to the close dialog method.
            var paginatedControllerResponse = await UserService.GetPaginatedUserRoles(new PaginationRequstDTO());

            users = paginatedControllerResponse.Content;
            filteredUsers = users;

            totalUsers = paginatedControllerResponse.TotalRecordCount;

            await base.OnInitializedAsync();
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

        public async void InviteUser()
        {
            await DialogService.OpenAsync<InviteUserModal>("Invite New User");
        }

        public async void DeleteUser(UserRoleView userRoleView)
        {
            await UserService.DeleteUserFromUserRoleView(userRoleView);
        }

        public async void CloseDialog(dynamic result)
        {
            if (result != null && !result.IsCancellation) // if the modal is exited from any fashion other then cancel or save then the result will be null
            {
                await UserInviteService.SendInviteEmail(result.Data);
            }
        }
    }
}
