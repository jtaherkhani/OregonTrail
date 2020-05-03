using Microsoft.AspNetCore.Identity;


namespace OregonTrail.UI.Shared.DTOs
{
    /// <summary>
    /// Data transfer class to handle confirmation of an email address.
    /// </summary>
    public class ConfirmEmailDTO
    {
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public bool HasErrors { get; set; }
    }
}
