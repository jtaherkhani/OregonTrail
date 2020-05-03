using Microsoft.AspNetCore.Components;
using OregonTrail.Models.Shared;
using Radzen;

namespace OregonTrail.UI.Client.Pages.Modals
{
    public class InviteUserModalCode : ComponentBase
    {
        [Inject]
        public DialogService DialogService { get; set; }

        public UserInvite NewUserInvite { get; set; } = new UserInvite();

        public void Save()
        {
            DialogService.Close(DialogResult<UserInvite>.Ok(NewUserInvite));
        }

        public void Cancel()
        {
            DialogService.Close(DialogResult<UserInvite>.Cancel());
        }

    }
}
