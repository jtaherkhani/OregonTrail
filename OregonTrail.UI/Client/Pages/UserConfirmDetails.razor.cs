using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using OregonTrail.UI.Client.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OregonTrail.UI.Client.Pages
{
    public class UserConfirmDetailsCode : ComponentBase
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        UserInviteService UserInviteService { get; set; }

        private string Token;
        private string EmailAddress;

        protected override async Task OnInitializedAsync()
        {
            var absoluteUri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            ParseQueryOnInitialized(absoluteUri);

            if (!IsExpectedAuthenticationProvided())
            {
                NavigationManager.NavigateTo("/"); // navigate back to the home page if the token authentication is wrong need to append an error message if this is the case - let's try to do this from the front-end
            }
            else
            {
                var confirmEmailDTO = await UserInviteService.ConfirmEmail(Token, EmailAddress);
                
                if (confirmEmailDTO.HasErrors)
                {
                    NavigationManager.NavigateTo("/"); // navigate back to the home page if the token authentication does this still show the modal even if the page redirects? Or do the confirm then get redirected
                }
                
                NavigationManager.NavigateTo(absoluteUri.GetLeftPart(UriPartial.Path));
            }

            await base.OnInitializedAsync();
        }

        private void ParseQueryOnInitialized(Uri uri)
        {
            var urlQuery = QueryHelpers.ParseQuery(uri.Query);

            if (urlQuery.TryGetValue("accessToken", out var accessTokenParam)) // todo: access token needs to be defined as a constant
            {
                Token = accessTokenParam.First();
            }

            if (urlQuery.TryGetValue("email", out var emailParam))
            {
                EmailAddress = emailParam.First();
            }
        }

        private bool IsExpectedAuthenticationProvided()
        {
            return !string.IsNullOrEmpty(Token)
                && !string.IsNullOrEmpty(EmailAddress);
        }
    }
}
