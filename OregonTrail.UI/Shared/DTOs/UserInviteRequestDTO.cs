using OregonTrail.Models.Shared;
using System;

namespace OregonTrail.UI.Shared.DTOs
{
    /// <summary>
    /// Dto class utlitized to send user invite requests to the server.
    /// </summary>
    public class UserInviteRequestDTO
    {
        public UserInvite UserInvite { get; set; }
        public Uri ConfirmationUri { get; set; }
    }
}
