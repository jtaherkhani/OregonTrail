using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OregonTrail.Data.Context;
using OregonTrail.Data.Services;
using OregonTrail.Models.Shared;
using OregonTrail.UI.Server.Services;
using OregonTrail.UI.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OregonTrail.UI.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class UserInviteController : Controller
    {
        private readonly UserInviteDataService DataService; // todo: We need this to expand token functionality to allow time fence validation
        private readonly UserManager<IdentityUser> UserManager;
        private readonly SendGridService SendGridEmailService;


        public UserInviteController(OregonTrailDBContext dbContext, UserManager<IdentityUser> userManager, IOptions<SendGridOptions> options)
        {
            DataService = new UserInviteDataService(dbContext);
            UserManager = userManager;
            SendGridEmailService = new SendGridService(options);
        }

        [HttpPost]
        public async Task<ControllerResponseDTO<UserInvite>> SendInvite(UserInviteRequestDTO userInviteRequestDTO)
        {
            if (userInviteRequestDTO == null)
            {
                throw new ArgumentNullException(nameof(userInviteRequestDTO));
            }

            var newUserUnconfirmedEmail = new IdentityUser()
            {
                UserName = Guid.NewGuid().ToString(), // create a dummy user name that will be overridden when the user confirms their email
                Email = userInviteRequestDTO.UserInvite.EmailAddress
            };

            var userCreationResult = await UserManager.CreateAsync(newUserUnconfirmedEmail);

            if (userCreationResult.Succeeded)
            {
                var accessToken = await UserManager.GenerateEmailConfirmationTokenAsync(newUserUnconfirmedEmail);

                var uriBuilder = new UriBuilder(userInviteRequestDTO.ConfirmationUri);
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    { "accessToken", accessToken },
                    { "email", newUserUnconfirmedEmail.Email}
                });
                uriBuilder.Query = content.ReadAsStringAsync().Result;

                var response = await SendGridEmailService.SendEmailInvite(userInviteRequestDTO.UserInvite, uriBuilder.Uri);

                if (response.StatusCode == HttpStatusCode.Accepted)
                {
                    return new ControllerResponseDTO<UserInvite>()
                    {
                        Content = userInviteRequestDTO.UserInvite
                    };
                }
                else
                {
                    return new ControllerResponseDTO<UserInvite>()
                    { 
                        ErrorMessage = string.Format("The SendGrid email service is currently erroring with code {0}, please contact your administrator", response.StatusCode.ToString()),
                        Content = userInviteRequestDTO.UserInvite
                    };
                }
            }
            else
            {
                return new ControllerResponseDTO<UserInvite>()
                { 
                    ErrorMessage = userCreationResult.Errors.First().Description, // Only the first error will be surfaced to the user.
                    Content = userInviteRequestDTO.UserInvite
                };
            }
        }

        [HttpPost]
        public async Task<ControllerResponseDTO<ConfirmEmailDTO>> ConfirmEmail(ConfirmEmailDTO confirmEmailDTO)
        {
            var user = await UserManager.FindByEmailAsync(confirmEmailDTO.EmailAddress);
            
            var identityResult = await UserManager.ConfirmEmailAsync(user, confirmEmailDTO.Token);

            if (identityResult.Succeeded)
            {
                return new ControllerResponseDTO<ConfirmEmailDTO>()
                {
                    Content = confirmEmailDTO
                };
            }

            confirmEmailDTO.HasErrors = true;

            return new ControllerResponseDTO<ConfirmEmailDTO>()
            {
                ErrorMessage = identityResult.Errors.First().Description,
                Content = confirmEmailDTO
            };
        }
    }
}
