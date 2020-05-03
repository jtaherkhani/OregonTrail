using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Identity;
using OregonTrail.Models.Shared;
using OregonTrail.UI.Shared.DTOs;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OregonTrail.UI.Client.Services
{
    public class UserInviteService : ServerService
    {
        private UriBuilder ConfirmationUriBuilder;

        public UserInviteService(HttpClient httpClient, SweetAlertService sweetAlertService)
          : base(httpClient, sweetAlertService)
        {
            ConfirmationUriBuilder = new UriBuilder(httpClient.BaseAddress);
            var builder = new UriBuilder(httpClient.BaseAddress);
            builder.Path += "api/UserInvite/";
            ControllerUri = builder.Uri;
        }

        public async Task<UserInvite> SendInviteEmail(UserInvite userInvite)
        {
            var path = "SendInvite";

            var userInviteRequestDto = new UserInviteRequestDTO()
            { 
                ConfirmationUri = CreateConfirmationUri(),
                UserInvite = userInvite 
            };

            var controllerResponseDTO = await Post<UserInviteRequestDTO>(path, userInviteRequestDto);

            return controllerResponseDTO.Content.UserInvite;
        }

        public async Task<ConfirmEmailDTO> ConfirmEmail(string token, string email)
        {
            var path = "ConfirmEmail";

            var confirmEmailDTO = new ConfirmEmailDTO()
            {
                EmailAddress = email,
                Token = token
            };

            var controllerResponseDTO = await Post<ConfirmEmailDTO>(path, confirmEmailDTO);

            return controllerResponseDTO.Content;
        }

        private Uri CreateConfirmationUri()
        {
            ConfirmationUriBuilder.Path += "users/confirm";
            return ConfirmationUriBuilder.Uri;
        }
    }
}
