using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace OregonTrail.UI.Client.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// Creates an anonymous data object containing all values of an enum to filer a radzen grid.
        /// </summary>
        /// <typeparam name="T">The enum type to be added to the drop down.</typeparam>
        /// <returns>An List of anonymous data objets containing all values of an enum in a format understood by a radzen data grid.</returns>
        public static List<object> CreateEnumDropDown<T>()
        {
            var enumValues = Enum.GetValues(typeof(T)).Cast<T>();

            return enumValues.Select(x => new
            {
                Text = $"{x}",
                Value = x
            }).ToList<object>();
        }
    }
}
