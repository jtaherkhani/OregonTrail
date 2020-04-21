using OregonTrail.UI.Client.Models;
using System.Collections.Generic;

namespace OregonTrail.UI.Client.Helpers
{
    /// <summary>
    /// Helper class created to handle rendering and creation of Menu items.
    /// </summary>
    public static class MenuItemHelper
    {

        /// <summary>
        /// Creates the menu navigation hierarchy that is displayed from the front-end.
        /// </summary>
        /// <returns></returns>
        public static List<MenuItem> CreateMenuNavigation()
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Text = "Home",
                    IconClass = "home",
                    URL="/"
                },

                CreateInventoryNaviagtion()
            };
        }

        /// <summary>
        /// Creates the Inventory menu item with nestesed navigation.
        /// </summary>
        /// <returns></returns>
        private static MenuItem CreateInventoryNaviagtion()
        {
            return new MenuItem()
            {
                Text = "Inventory",
                IconClass = "gamepad",
                ExpandedByDefault = true,
                SubMenuItems = new List<MenuItem>()
                {
                    new MenuItem()
                    {
                        Text = "Items",
                        URL = "/items"
                    }
                }
            };
        }
    }
}
