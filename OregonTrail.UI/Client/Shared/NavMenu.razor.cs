using Microsoft.AspNetCore.Components;
using OregonTrail.UI.Client.Helpers;
using OregonTrail.Models.UI;
using System.Collections.Generic;

namespace OregonTrail.UI.Client.Shared
{
    public class NavMenuCode : ComponentBase
    {
        public List<MenuItem> MenuItems;

        public bool collapseNavMenu = true;

        public string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        public void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        protected override void OnInitialized()
        {
            MenuItems = MenuItemHelper.CreateMenuNavigation();

            base.OnInitialized();
        }
    }
}
