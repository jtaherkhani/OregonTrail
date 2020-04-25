using OregonTrail.Models.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OregonTrail.Models.Shared
{
    /// <summary>
    /// Model for representing both the user and their role in a single data grid. 
    /// </summary>
    public class UserRoleView
    {
        // User fields
        public string UserName { get; set; }
        public string Email { get; set; }

        // Role fields
        public Role? Role { get; set; } // Role can be undefined based on Identity server data structures (even if in practice it will not happen).

    }
}
