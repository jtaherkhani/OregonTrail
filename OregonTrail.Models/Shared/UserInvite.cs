using System.ComponentModel.DataAnnotations;

namespace OregonTrail.Models.Shared
{
    /// <summary>
    /// Stores the details of an invite. 
    /// </summary>
    public class UserInvite
    {
        [EmailAddress]
        [Required]
        [MaxLength(400)]
        public string EmailAddress { get; set; }
    }
}
