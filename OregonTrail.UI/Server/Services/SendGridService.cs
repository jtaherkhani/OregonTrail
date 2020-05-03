using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using OregonTrail.Data.Context;
using OregonTrail.Models.Shared;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OregonTrail.UI.Server.Services
{
    /// <summary>
    /// Service class for communicating with SendGrid for email services.
    /// </summary>
    public class SendGridService
    {
        private readonly SendGridOptions SendGridOptions;

        public SendGridService(IOptions<SendGridOptions> options)
        {
            SendGridOptions = options.Value;
        }

        public async Task<Response> SendEmailInvite(UserInvite invite, Uri confirmationUri)
        {
            var client = new SendGridClient(SendGridOptions.APIKey);

            var message = new SendGridMessage();

            message.SetFrom(SendGridOptions.FromEmail);
            message.AddTo(invite.EmailAddress);
            message.SetTemplateId(SendGridOptions.TemplateId);

            var emailTemplateData = new EmailTemplateData()
            {
                FirstName = "Josh",
                ConfirmationUri = confirmationUri
            };

            message.SetTemplateData(emailTemplateData);
            message.SetClickTracking(true, true);

            return await client.SendEmailAsync(message);

        }
    }

    /// <summary>
    /// Contains the data required for the Email template, class utilized by the SendGridService class only.
    /// </summary>
    public class EmailTemplateData
    {
        public string FirstName{ get; set;}
        public Uri ConfirmationUri { get; set; }
    }
}
