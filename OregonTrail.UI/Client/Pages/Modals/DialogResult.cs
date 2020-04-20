using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OregonTrail.UI.Client.Pages.Modals
{
    /// <summary>
    /// Created due to Radzen Dialog returning a dynamic type as the dialog result, this will add a readable structure to the modals.
    /// </summary>
    public class DialogResult<T>
    {
        /// <summary>
        /// True if the user cancelled out of the dialog; otherwise false
        /// </summary>
        public bool IsCancellation { get; set; }

        /// <summary>
        /// Data of anytype can be provided in the result.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Creates a dialog result that represents when the modal is correctly submitted.
        /// </summary>
        /// <param name="data">The data to be stored in the result.</param>
        /// <returns>The dialog result.</returns>
        public static DialogResult<T> Ok(T data)
        {
            return new DialogResult<T>
            {
                IsCancellation = false,
                Data = data
            };
        }

        /// <summary>
        /// Creates a dialog result that represents whent the data entry on the modal is cancelled.
        /// </summary>
        /// <returns>The dialog result.</returns>
        public static DialogResult<T> Cancel()
        {
            return new DialogResult<T>
            {
                IsCancellation = true
            };
        }
    }
}
