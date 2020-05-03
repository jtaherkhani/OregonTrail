using System;
using System.Collections.Generic;
using System.Text;

namespace OregonTrail.Models.Shared
{
    /// <summary>
    /// Expected options for SendGrid
    /// </summary>
    public class SendGridOptions
    {
        public string APIKey { get; set; }
        public string TemplateId { get; set; }
        public string FromEmail { get; set; }    
    }
}
