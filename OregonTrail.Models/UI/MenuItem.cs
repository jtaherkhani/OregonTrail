using System.Collections.Generic;


namespace OregonTrail.Models.UI
{
    /// <summary>
    /// Defines naviagatible menu items displayed to the website.
    /// </summary>
    public class MenuItem
    {
        public string Text { get; set; }
        public string URL { get; set; }
        public string IconClass { get; set; }
        public bool ExpandedByDefault { get; set; }
        public List<MenuItem> SubMenuItems { get; set; }
    }
}
